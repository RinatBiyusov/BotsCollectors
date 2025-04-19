using System;
using UnityEngine;

public class ResourceEventDispatcher : MonoBehaviour, IObserverOres
{
    [SerializeField] private WarehouseOres _wareHouse;

    protected WarehouseOres WareHouse => _wareHouse;

    public event Action<OreType, int> Changed;

    private void OnEnable()
    {
        _wareHouse.Collected += UpdateOres;
    }

    private void OnDisable()
    {
        _wareHouse.Collected -= UpdateOres;
    }

    public void UpdateOres(OreType type, int amount)
    {
        Changed?.Invoke(type, amount);
    }
}