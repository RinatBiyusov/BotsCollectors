using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public event Action<Resource> Collected;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Rigidbody = _rigidbody;
    }

    public void NotifyResourceCollected() =>
        Collected?.Invoke(this);
}