using UnityEngine;
using System;

public class WorkersSpawner : GenericSpawner<Worker>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _startAmountWorkers = 3; // Добавить зависимость от этого
    
    public event  Action<Worker> WorkerSpawned;
    
    private void Start()
    {
        foreach (Transform position in _spawnPoints)
        {
            Worker worker = TakeObject();
            WorkerSpawned?.Invoke(worker);
            worker.Init(position.position);
            worker.transform.position = position.position;
            worker.transform.rotation = Quaternion.identity;
        }
    }
}
