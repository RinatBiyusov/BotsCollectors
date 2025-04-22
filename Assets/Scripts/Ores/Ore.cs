using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ore : MonoBehaviour
{
    [SerializeField] private OreType _type;
    [field: SerializeField] public WorkerTarget WorkerTarget { get; private set; }

    public OreType Type => _type;

    private Rigidbody _rigidbody;
    private bool _isCarried = false;

    public event Action<Ore> Collected;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Rigidbody = _rigidbody;
    }

    public bool TryPickUp()
    {
        if (_isCarried)
            return false;

        _isCarried = true;
        return true;
    }

    public void Drop() =>
        _isCarried = false;

    public void NotifyResourceCollected() =>
        Collected?.Invoke(this);
}