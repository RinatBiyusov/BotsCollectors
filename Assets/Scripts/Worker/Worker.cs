using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Worker : MonoBehaviour
{
    [SerializeField] private WorkerMover _workerMover;
    [SerializeField] private WorkerResourcesPicker _workerResourcesPicker;
    
    private Vector3 _startPosition;

    public event Action<Worker> ResourceCollected;

    public bool  IsWorking { get; private set; }
    
    public void Init(Vector3 startPosition) =>
        _startPosition = startPosition;
    
    private void OnEnable()
    {
        _workerResourcesPicker.ResourcePicked += BringResources;
    }

    private void OnDisable()
    {
        _workerResourcesPicker.ResourcePicked -= BringResources;
    }

    private void BringResources() =>
        ResourceCollected?.Invoke(this);

    public void DefineJob(Vector3 target)
    {
        IsWorking =  true;
        _workerMover.SetTarget(target);
    }

    public void DropResource()
    {
        _workerMover.SetTarget(_startPosition);
        _workerResourcesPicker.Drop();
        IsWorking = false;
    }
}