using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private BaseControlPanelUI _controlPanelUI;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private WorkersSpawner _workersSpawner;
    [SerializeField] private ResourcesCollector _resourcesCollector;
    [SerializeField] private WareHouseResources _wareHouseResources;

    private readonly List<Worker> _workers = new List<Worker>();
    private List<Resource> _resources = new List<Resource>();

    private void OnEnable()
    {
        _controlPanelUI.PlayButtonClicked += DistributeWorkers;
        _workersSpawner.WorkerSpawned += AddWorker;
    }

    private void OnDisable()
    {
        _controlPanelUI.PlayButtonClicked -= DistributeWorkers;
        _workersSpawner.WorkerSpawned -= AddWorker;

        foreach (Worker worker in _workers)
            worker.ResourceCollected -= SendWorkerToWareHouse;
    }

    public void DistributeWorkers()
    {
        FindResources();

        if (_resources.Count == 0)
            return;

        for (int i = 0; i < _workers.Count; i++)
        {
            if (_workers[i].IsWorking || _resources[i] == null)
                continue;
            
            _workers[i].DefineJob(_resources[i].transform.position);
            _workers[i].ResourceCollected += SendWorkerToWareHouse;
        }
    }

    private void FindResources()
    {
        _resources.Clear();
        _resources = _scanner.Scan();
    }
    
    private void SendWorkerToWareHouse(Worker worker) =>
        worker.DefineJob(_resourcesCollector.transform.position);

    private void AddWorker(Worker worker) =>
        _workers.Add(worker);
}