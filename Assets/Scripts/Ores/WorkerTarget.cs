using UnityEngine;

public class WorkerTarget : MonoBehaviour
{
    private Worker _targetedWorker;

    private void OnDisable()
    {
        _targetedWorker = null;
    }

    public void SetTarget(Worker worker) =>
        _targetedWorker = worker;

    public bool HasAssignedWorker() =>
        _targetedWorker != null;
    
    public bool IsAssignedTo(Worker worker) =>
        _targetedWorker == worker;
    
    public void ResetTarget() =>
        _targetedWorker = null;
}