using System;
using UnityEngine;

public class OreEventDispatcher : MonoBehaviour
{
    [SerializeField] private WarehouseOre _wareHouse;

    public event Action<OreType, int> Changed;

    private void OnEnable()
    {
        _wareHouse.Changed += UpdateOres;
    }

    private void OnDisable()
    {
        _wareHouse.Changed -= UpdateOres;
    }

    private void UpdateOres(OreType type, int amount) =>
        Changed?.Invoke(type, amount);
}