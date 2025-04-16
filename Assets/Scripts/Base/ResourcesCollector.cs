using UnityEngine;
using System;

public class ResourcesCollector : MonoBehaviour
{
    [SerializeField] private Collider _collectorZone;
    
    public event Action<Resource> Collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            resource.NotifyResourceCollected();
            Collected?.Invoke(resource);
        }
    }
}