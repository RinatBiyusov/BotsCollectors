using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    [Header("Core Components")] [SerializeField]
    private Scanner _scanner;

    [SerializeField] private OreCollector _oreCollector;
    [SerializeField] private WarehouseOre _warehouseOre;
    [SerializeField] private WorkersSpawner _workersSpawner;

    
    [Header("Settings")] [SerializeField] private int _maxAmountWorkers;
    [SerializeField] private Transform[] _workersSpawnPoints;

    [Header("UI Invokers")] [SerializeField]
    private UIButtonInvoker _buttonSendToWork;

    [SerializeField] private UIButtonInvoker _buttonHireWorker;
    [SerializeField] private UIButtonInvoker _buttonSetFlag;

    private readonly List<Worker> _workers = new List<Worker>();
    private List<Ore> _ores = new List<Ore>();
    
    private void Awake()
    {
        _buttonSendToWork.Bind(new SendWorkersCommand(this));
        _buttonHireWorker.Bind(new HireWorkerCommand(this));
        // _buttonSetFlag.Bind(new SetFlagCommand(this));
    }

    private void OnEnable()
    {
        _workersSpawner.WorkerSpawned += AddWorker;
        Game.GameStarted += GameOnGameStarted;
    }

    private void OnDisable()
    {
        _workersSpawner.WorkerSpawned -= AddWorker;
        Game.GameStarted -= GameOnGameStarted;

        foreach (Worker worker in _workers)
            worker.ResourceCollected -= SendWorkerToWareHouse;
    }

    private void GameOnGameStarted() =>
        _workersSpawner.Init(this);

    public void DistributeWorkers()
    {
        _ores = _scanner.Scan();

        if (_ores.Count == 0)
            return;

        foreach (Worker worker in _workers)
        {
            if (worker.IsWorking)
                continue;

            Ore targetOre = GetFreeOre();

            if (targetOre == null)
                break;

            targetOre.WorkerTarget.SetTarget(worker);
            worker.DefineJob(targetOre.transform.position);
            worker.ResourceCollected -= SendWorkerToWareHouse;
            worker.ResourceCollected += SendWorkerToWareHouse;
        }
    }

    public void HireWorker()
    {
        if (_workers.Count >= _maxAmountWorkers)
            return;

        _workersSpawner.HireWorker(this);
    }
    
    private Ore GetFreeOre()
    {
        foreach (var ore in _ores)
        {
            if (ore != null && !ore.WorkerTarget.HasAssignedWorker())
                return ore;
        }

        return null;
    }

    private void SendWorkerToWareHouse(Worker worker) =>
        worker.DefineJob(_oreCollector.transform.position);

    private void AddWorker(Worker worker)
    {
        if (worker.IsOwnerOf(this) == false)
            return;

        worker.Init(_workersSpawnPoints[_workers.Count].position);
        worker.transform.position = _workersSpawnPoints[_workers.Count].position;
        _workers.Add(worker);
    }
}