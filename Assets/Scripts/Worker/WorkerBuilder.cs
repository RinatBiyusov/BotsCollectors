using UnityEngine;

public class WorkerBuilder : MonoBehaviour
{
    [SerializeField] private Base _prefabBase;
    [SerializeField] private WorkerMover _workerMover;
    
    public void BuildBase(Worker worker, Base oldBase, Transform flagTarget)
    {
        Base newBase = Instantiate(_prefabBase, flagTarget.position, Quaternion.identity);

        newBase.ClearFlag();
        newBase.ClearWorkers();
        oldBase.RemoveWorker(worker);
        
        worker.SetOwner(newBase);
        newBase.AddWorker(worker);
    }
}
