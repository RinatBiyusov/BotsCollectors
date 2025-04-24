using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMover : MonoBehaviour
{
    [SerializeField] private float _distanceToCheck;
    [SerializeField] private NavMeshAgent _agent;

    public event Action FlagTargetReached;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Flag flag))
        {
           flag.gameObject.SetActive(false); 
            FlagTargetReached?.Invoke();
        }
    }

    public void SetTarget(Vector3 target) =>
        MoveTo(target);

    private void MoveTo(Vector3 target) =>
        _agent.SetDestination(target);
}