using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMover : MonoBehaviour
{
    [SerializeField] private float _distanceToCheck;
    [SerializeField] private NavMeshAgent _agent;
    
    public void SetTarget(Vector3 target) =>
        MoveTo(target); 

    private void MoveTo(Vector3 target) =>
        _agent.SetDestination(target);
}