using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private OreCollector _oreCollector;
    [SerializeField] private WarehouseOre _warehouseOre;
    [SerializeField] private WorkersSpawner _workersSpawner;
    [SerializeField] private BaseControlPanelUI _controlPanelUI;

    private readonly List<Worker> _workers = new List<Worker>();
    private List<Ore> _ores = new List<Ore>();

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

    private void DistributeWorkers()
    {
        FindResources();

        if (_ores.Count == 0)
            return;

        for (int i = 0; i < _workers.Count; i++)
        {
            if (_workers[i].IsWorking || _ores[i] == null)
                continue;

            _workers[i].DefineJob(_ores[i].transform.position);
            _workers[i].ResourceCollected += SendWorkerToWareHouse;
        }
    }

    private void FindResources()
    {
        _ores.Clear();
        _ores = _scanner.Scan();
    }

    private void SendWorkerToWareHouse(Worker worker) =>
        worker.DefineJob(_oreCollector.transform.position);

    private void AddWorker(Worker worker) =>
        _workers.Add(worker);
}