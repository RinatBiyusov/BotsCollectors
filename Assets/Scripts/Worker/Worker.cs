using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Worker : MonoBehaviour
{
    [SerializeField] private WorkerMover _workerMover;
    [SerializeField] private WorkerResourcesPicker _workerResourcesPicker;

    private Vector3 _startPosition;
    private Base _ownerBase;
    private Transform _flagTarget;

    public event Action<Worker> ResourceCollected;

    public int NumberIndex { get; private set; }
    public bool IsWorking { get; private set; }
    public bool IsPlacingFlag { get; private set; }

    public void Init(Vector3 startPosition, int numberSpawn)
    {
        NumberIndex = numberSpawn;
        _startPosition = startPosition;
    }

    private void OnEnable()
    {
        _workerResourcesPicker.OrePicked += BringOres;
    }

    private void OnDisable()
    {
        _workerResourcesPicker.OrePicked -= BringOres;
    }

    public void SetFlagTarget(Transform flagTransform)
    {
        if (flagTransform == null) return;

        IsWorking = true;
        _flagTarget = flagTransform;
        _workerMover.SetTarget(flagTransform.position);
        _workerMover.FlagTargetReached += CreateNewBase;
    }

    public void DefineJob(Vector3 target)
    {
        IsWorking = true;
        _workerMover.SetTarget(target);
    }

    public void DropResource()
    {
        _workerMover.SetTarget(_startPosition);
        _workerResourcesPicker.Drop();
        IsWorking = false;
    }

    public void SetOwner(Base baseInstance) =>
        _ownerBase = baseInstance;

    public void AssignToSetFlag() =>
        IsPlacingFlag = true;

    public bool IsOwnerOf(Base baseInstance) =>
        baseInstance == _ownerBase;

    public bool HasResource() =>
        _workerResourcesPicker.IsHoldingResource();

    private void CreateNewBase()
    {
        _workerMover.FlagTargetReached -= CreateNewBase;
        Base newBase = Instantiate(_ownerBase, _flagTarget.position, Quaternion.identity);

        newBase.ClearFlag();
        newBase.ClearWorkers();
        _ownerBase.RemoveWorker(this);
        
        SetOwner(newBase);
        newBase.AddWorker(this);
        
        _workerMover.SetTarget(_startPosition);
        IsWorking = false;
    }

    private void BringOres() =>
        ResourceCollected?.Invoke(this);
}