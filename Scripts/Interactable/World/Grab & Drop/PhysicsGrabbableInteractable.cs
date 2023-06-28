using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsGrabbableInteractable : MonoBehaviour, IGrabbableInteractable
{
    [RequireInterface(typeof(IGrabbableInteractable))]
    [SerializeField] private Object _grabbableInteractableObject;
    private IGrabbableInteractable GrabbableInteractable => _grabbableInteractableObject as IGrabbableInteractable;
    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public UnityEvent<IInteractor> OnInteracted => GrabbableInteractable.OnInteracted;

    public void Interact(IInteractor interactor)
    {
        StartCoroutine(InteractCoroutine(interactor));
    }

    public IEnumerator InteractCoroutine(IInteractor interactor)
    {
        RigidbodyAccessor.IsKinematic = true;
        yield return GrabbableInteractable.InteractCoroutine(interactor);
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