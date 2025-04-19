using UnityEngine;
using System;

public class OresCollector : MonoBehaviour
{
    [SerializeField] private Collider _collectorZone;
    
    public event Action<Ore> Collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ore ore))
        {
            ore.NotifyResourceCollected();
            Collected?.Invoke(ore);
        }
    }
}