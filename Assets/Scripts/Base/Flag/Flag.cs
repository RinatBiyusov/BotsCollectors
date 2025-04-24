using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Flag : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Worker _workerBuilder;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveFlag(Vector3 newPosition) =>
        transform.position = newPosition;
}