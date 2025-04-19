using UnityEngine;
using System;

public class OreCollector : MonoBehaviour
{
    [SerializeField] private Collider _collectorZone;
    
    public event Action<Ore> Collected;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Ore ore))
        {
            ore.NotifyResourceCollected();
            Collected?.Invoke(ore);
        }
    }
}