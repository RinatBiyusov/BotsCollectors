using UnityEngine;

public class DropZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Worker worker))
            worker.DropResource();
    }
}