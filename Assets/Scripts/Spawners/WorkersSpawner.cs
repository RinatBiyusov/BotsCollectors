using UnityEngine;
using System;

public class WorkersSpawner : GenericSpawner<Worker>
{
    [SerializeField] private int _startAmountWorkers = 3;

    public event Action<Worker> WorkerSpawned;

    public void Init(Base baseInstance)
    {
        for (int i = 0; i < _startAmountWorkers; i++)
        {
            Worker worker = TakeObject();
            worker.SetOwner(baseInstance);
            WorkerSpawned?.Invoke(worker);
        }
    }

    public void HireWorker(Base baseInstance)
    {
        Worker worker = TakeObject();
        worker.SetOwner(baseInstance);
        WorkerSpawned?.Invoke(worker);
    }
}