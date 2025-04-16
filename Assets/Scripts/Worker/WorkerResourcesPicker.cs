using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody))]
public class WorkerResourcesPicker : MonoBehaviour
{
    [SerializeField] private Vector3 _holdOffset = new Vector3(0, 1.5f, 1f);

    private Resource _heldObject;

    public bool IsHoldsResource { get; private set; }

    public event Action ResourcePicked;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource))
            PickUp(resource);
    }

    private void PickUp(Resource resource)
    {
        if (IsHoldsResource)
            return;

        _heldObject = resource;

        if (resource != null)
            resource.Rigidbody.isKinematic = true;

        resource.transform.SetParent(transform);
        resource.transform.localPosition = _holdOffset;

        IsHoldsResource = true;

        ResourcePicked?.Invoke();
    }

    public void Drop()
    {
        if (!IsHoldsResource)
            return;

        _heldObject.transform.SetParent(null);

        _heldObject.Rigidbody.isKinematic = false;
        
        _heldObject = null;
        IsHoldsResource = false;
    }
}