using UnityEngine;
using System.Collections.Generic;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _radiusScan;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _size = 50;

    private Collider[] _colliders;
    private readonly List<Resource> _resources = new List<Resource>();

    private void Start()
    {
        _colliders = new Collider[_size];
    }

    public List<Resource> Scan()
    {
        _resources.Clear();
        
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radiusScan, _colliders, _layerMask);

        if (hitCount == 0)
            return null;
        
        for (int i = 0; i < hitCount; i++)
            _resources.Add(_colliders[i].GetComponent<Resource>());

        return _resources;
    }
}