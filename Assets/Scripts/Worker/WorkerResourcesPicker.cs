using System;
using UnityEngine;

public class WorkerResourcesPicker : MonoBehaviour
{
    [SerializeField] private Vector3 _holdOffset = new Vector3(0, 1.5f, 1f);
    [SerializeField] private Worker _ownerWorker;

    private Ore _heldObject;
    private bool _isHoldsResource;

    public event Action OrePicked;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ore ore))
            if (ore.WorkerTarget.IsAssignedTo(_ownerWorker))
                PickUp(ore);
    }

    public void Drop()
    {
        if (_isHoldsResource == false)
            return;

        _heldObject.transform.SetParent(null);

        _heldObject.WorkerTarget.ResetTarget();
        _heldObject.Drop();

        _heldObject = null;
        _isHoldsResource = false;
    }

    public bool IsHoldingResource() =>
        _isHoldsResource;

    private void PickUp(Ore ore)
    {
        if (_isHoldsResource || ore.TryPickUp() == false)
            return;

        _heldObject = ore;

        if (ore != null)
            ore.Rigidbody.isKinematic = true;

        ore.transform.SetParent(transform);
        ore.transform.localPosition = _holdOffset;

        _isHoldsResource = true;

        OrePicked?.Invoke();
    }
}