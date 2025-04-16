using System;
using UnityEngine;

public class WareHouseResources : MonoBehaviour
{
    [SerializeField] private ResourcesCollector _collector;

    private int _goldAmount;
    private int _coalAmount;
    private int _copperAmount;
    
    public event Action<int> GoldAmountChanged;
    public event Action<int> CoalAmountChanged;
    public event Action<int> CopperAmountChanged;

    private void OnEnable()
    {
        _collector.Collected += ReceiveResource;
    }

    private void OnDisable()
    {
        _collector.Collected -= ReceiveResource;
    }

    private void ReceiveResource(Resource resource)
    {
        switch (resource)
        {
            case Gold:
                _goldAmount++;
                GoldAmountChanged?.Invoke(_goldAmount);
                break;
            
            case Coal:
                _coalAmount++;
                CoalAmountChanged?.Invoke(_coalAmount);
                break;
            
            case Copper:
                _copperAmount++;
                CopperAmountChanged?.Invoke(_copperAmount);
                break;
            
            default:
                Debug.Log("Resource Not Found");
                break;
        }
    }
}