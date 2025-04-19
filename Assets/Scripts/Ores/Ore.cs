using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ore : MonoBehaviour
{
    [SerializeField] private OreType _type;    
    
    public  OreType Type => _type;
     
    private Rigidbody _rigidbody;

    public event Action<Ore> Collected;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Rigidbody = _rigidbody;
    }

    public void NotifyResourceCollected() =>
        Collected?.Invoke(this);
}