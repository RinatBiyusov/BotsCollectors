using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody))]
public class WorkerResourcesPicker : MonoBehaviour
{
    [SerializeField] private Vector3 _holdOffset = new Vector3(0, 1.5f, 1f);

    private Ore _heldObject;
    private bool _isHoldsResource;
    
    public event Action ResourcePicked;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ore resource))
            PickUp(resource);
    }

    public void Drop()
    {
        if (_isHoldsResource == false)
            return;

        _heldObject.transform.SetParent(null);

        _heldObject.Rigidbody.isKinematic = false;

        _heldObject = null;
        _isHoldsResource = false;
    }
    
    private void PickUp(Ore ore)
    {
        if (_isHoldsResource)
            return;

        _heldObject = ore;

        if (ore != null)
            ore.Rigidbody.isKinematic = true;

        ore.transform.SetParent(transform);
        ore.transform.localPosition = _holdOffset;

        _isHoldsResource = true;

        ResourcePicked?.Invoke();
    }
}