using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsGrabbableInteractable : MonoBehaviour, IGrabbableInteractable
{
    [SerializeField] private bool _disablePhysicsOnGrab = true;
    [SerializeField] private bool _disableCollisionOnGrab = true;

    [RequireInterface(typeof(IGrabbableInteractable))]
    [SerializeField] private Object _grabbableInteractableObject;
    private IGrabbableInteractable GrabbableInteractable => _grabbableInteractableObject as IGrabbableInteractable;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public Transform Transform => GrabbableInteractable.Transform;
    public UnityEvent<IInteractionHandler> OnInteracted => GrabbableInteractable.OnInteracted;

    public bool Interact(IInteractionHandler interactionHandler)
    {
        if (interactionHandler is not IGrabInteractionHandler) return false;
        StartCoroutine(InteractCoroutine(interactionHandler));
        return true;
    }

    public IEnumerator InteractCoroutine(IInteractionHandler interactionHandler)
    {
        RigidbodyAccessor.IsKinematic = _disablePhysicsOnGrab;
        if (_disableCollisionOnGrab)
            RigidbodyAccessor.DisableRigidbodyCollisions();
        yield return GrabbableInteractable.InteractCoroutine(interactionHandler);
    }           

    public IEnumerator GrabCoroutine(Transform grabberParent, System.Func<float, float> getGrabSpeed)
    {
        yield return GrabbableInteractable.GrabCoroutine(grabberParent, getGrabSpeed);
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, float grabTotalTime)
    {
        yield return GrabbableInteractable.GrabCoroutine(grabberParent, grabTotalTime);
    }

}