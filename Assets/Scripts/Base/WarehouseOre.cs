using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WarehouseOres : MonoBehaviour
{
    [SerializeField] private OreCollector _collector;
    [SerializeField] private List<OreStrategy> _oresStrategy;

    private Dictionary<OreType, OreStrategy> _strategyMap;
    private Dictionary<OreType, int> _counterMap;

    public event Action<OreType, int> Collected;

    private void Awake()
    {
        _strategyMap = _oresStrategy.ToDictionary(s => s.OreType, s => s);
        _counterMap = _oresStrategy.ToDictionary(s => s.OreType, s => 0);
    }

    private void Start()
    {
        foreach (var VARIABLE in _counterMap)
        {
            Debug.Log(VARIABLE.Key + " : " + VARIABLE.Value);
        }
    }

    private void OnEnable()
    {
        _collector.Collected += ReceiveResource;
    }

    private void OnDisable()
    {
        _collector.Collected -= ReceiveResource;
    }

    private void ReceiveResource(Ore ore)
    {
        if (_strategyMap.TryGetValue(ore.Type, out var strategy))
            strategy.Process(this, ore.Type);
    }

    public void AddOre(OreType oreType)
    {
        if (_counterMap.TryGetValue(oreType, out int currentAmount))
        {
            _counterMap[oreType] = currentAmount + 1;
            Collected?.Invoke(oreType, currentAmount);
        }
    }
}