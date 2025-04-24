using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;

public class WarehouseOre : MonoBehaviour
{
    [SerializeField] private OreCollector _collector;
    [SerializeField] private List<OreStrategy> _oresStrategy;

    private Dictionary<OreType, OreStrategy> _strategyMap;
    private Dictionary<OreType, int> _counterMap;

    public event Action<OreType, int> Changed;

    private void Awake()
    {
        _strategyMap = _oresStrategy.ToDictionary(s => s.OreType, s => s);
        _counterMap = _oresStrategy.ToDictionary(s => s.OreType, s => 0);
    }

    private void OnEnable()
    {
        _collector.Collected += ReceiveResource;
    }

    private void OnDisable()
    {
        _collector.Collected -= ReceiveResource;
    }

    public void AddOre(OreType oreType)
    {
        if (_counterMap.TryGetValue(oreType, out int currentAmount))
        {
            _counterMap[oreType] = currentAmount + 1;
            Changed?.Invoke(oreType, _counterMap[oreType]);
        }
    }

    public void SpendOre(OreType oreType, int amount)
    {
        _counterMap[oreType] -= amount;
        Changed?.Invoke(oreType, _counterMap[oreType]);
    }

    public ReadOnlyDictionary<OreType, int> CountAmountOre() =>
        new ReadOnlyDictionary<OreType, int>(_counterMap);

    private void ReceiveResource(Ore ore)
    {
        if (_strategyMap.TryGetValue(ore.Type, out var strategy))
            strategy.Process(this, ore.Type);
    }
}