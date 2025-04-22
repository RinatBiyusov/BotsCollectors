using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Worker : MonoBehaviour
{
    [SerializeField] private WorkerMover _workerMover;
    [SerializeField] private WorkerResourcesPicker _workerResourcesPicker;

    private Vector3 _startPosition;
    private Base _ownerBase;

    public event Action<Worker> ResourceCollected;

    public bool IsWorking { get; private set; }

    public void Init(Vector3 startPosition)
    {
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

    public void SetOwner(Base baseInstance)
    {
        _ownerBase = baseInstance;
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

    public bool IsOwnerOf(Base baseInstance) =>
        baseInstance == _ownerBase;

    public bool HasResource() =>
        _workerResourcesPicker.IsHoldingResource();

    private void BringOres() =>
        ResourceCollected?.Invoke(this);
}