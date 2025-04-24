using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [Header("Core Components")] [SerializeField]
    private Scanner _scanner;

    [SerializeField] private FlagPlacementHandler _placementHandler;
    [SerializeField] private OreCollector _oreCollector;
    [SerializeField] private WarehouseOre _warehouseOre;
    [SerializeField] private WorkersSpawner _workersSpawner;

    [Header("Settings")] [SerializeField] private int _maxAmountWorkers;
    [SerializeField] private Transform[] _workersSpawnPoints;

    [Header("UI Invokers")] [SerializeField]
    private UIButtonInvoker _buttonSendToWork;

    [SerializeField] private UIButtonInvoker _buttonHireWorker;
    [SerializeField] private UIButtonInvoker _buttonSetFlag;

    private readonly int _costToCreateWorker = 3;
    private readonly int _costToCreateBase = 5;
    private Worker[] _workers = new Worker[5];

    private List<Ore> _ores = new List<Ore>();

    public Flag CurrentFlag { get; private set; }

    private void Awake()
    {
        _workers = new Worker[_maxAmountWorkers];

        _buttonSendToWork.Bind(new SendWorkersCommand(this));
        _buttonHireWorker.Bind(new HireWorkerCommand(this));
        _buttonSetFlag.Bind(new SetFlagCommand(this, _placementHandler));
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
        {
            if (worker == null)
                continue;

            worker.ResourceCollected -= SendWorkerToWareHouse;
        }
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
            if (worker == null)
                continue;

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

    public bool CanBuildBase()
    {
        ReadOnlyDictionary<OreType, int> amountOres = _warehouseOre.CountAmountOre();

        foreach (var map in amountOres)
        {
            if (map.Value >= _costToCreateBase)
            {
                _warehouseOre.SpendOre(map.Key, _costToCreateBase);
                return true;
            }
        }

        return false;
    }

    public void SetFlag(Flag flag) =>
        CurrentFlag = flag;

    public void ClearFlag() =>
        CurrentFlag = null;

    public void SendWorkerToCreateNewBase()
    {
        Worker worker = TakeFreeWorker();

        if (worker == null)
            return;

        worker.AssignToSetFlag();
        worker.SetFlagTarget(CurrentFlag.transform);
    }

    public void HireWorker()
    {
        int countNotNull = _workers.Count(worker => worker != null);

        if (countNotNull == _maxAmountWorkers)
            return;

        ReadOnlyDictionary<OreType, int> amountOres = _warehouseOre.CountAmountOre();

        foreach (var map in amountOres)
        {
            if (map.Value >= _costToCreateWorker)
            {
                _warehouseOre.SpendOre(map.Key, _costToCreateWorker);
                _workersSpawner.HireWorker(this);
                break;
            }
        }
    }

    public void AddWorker(Worker worker)
    {
        if (worker.IsOwnerOf(this) == false)
            return;

        int index = Array.FindIndex(_workers, workerInstance => workerInstance == null);
        
        worker.Init(_workersSpawnPoints[index].position, index);
        worker.transform.position = _workersSpawnPoints[index].position;
        
        _workers[worker.NumberIndex] = worker;
    }

    public void ClearWorkers() =>
        Array.Clear(_workers, 0, _workers.Length);

    public void RemoveWorker(Worker worker)
    {
        for (int i = 0; i < _workers.Length; i++)
        {
            if (_workers[i] == worker)
            {
                Debug.Log(i);
                _workers[i] = null;
                break;
            }
        }
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

    private Worker TakeFreeWorker()
    {
        foreach (var worker in _workers)
        {
            if (worker.IsWorking == false)
                return worker;
        }

        return null;
    }
}