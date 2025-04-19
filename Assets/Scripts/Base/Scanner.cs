using UnityEngine;
using System.Collections.Generic;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _radiusScan;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _size = 50;

    private Collider[] _colliders;

    private void Start()
    {
        _colliders = new Collider[_size];
    }

    public List<Ore> Scan()
    {
        List<Ore> ores = new List<Ore>();
        
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radiusScan, _colliders, _layerMask);

        if (hitCount == 0)
            return null;
        
        for (int i = 0; i < hitCount; i++)
            ores.Add(_colliders[i].GetComponent<Ore>());

        return ores;
    }
}