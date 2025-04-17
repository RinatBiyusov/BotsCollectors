using UnityEngine;
using System;

public class WorkersSpawner : GenericSpawner<Worker>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _startAmountWorkers = 3;
    
    public event  Action<Worker> WorkerSpawned;
    
    private void Start()
    {
        for (int i = 0; i < _startAmountWorkers; i++)
        {
            if (_spawnPoints[i] == null)
                break;
            
            Worker worker = TakeObject();
            WorkerSpawned?.Invoke(worker);
            worker.Init(_spawnPoints[i].position);
            worker.transform.position = _spawnPoints[i].position;
            worker.transform.rotation = Quaternion.identity;
        }
    }
}
