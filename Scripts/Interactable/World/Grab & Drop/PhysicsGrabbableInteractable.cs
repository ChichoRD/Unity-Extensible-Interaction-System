using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsGrabbableInteractable : MonoBehaviour, IGrabbableInteractable
{
    [RequireInterface(typeof(IGrabbableInteractable))]
    [SerializeField] private Object _grabbableInteractableObject;
    private IGrabbableInteractable GrabbableInteractable => _grabbableInteractableObject as IGrabbableInteractable;

    [SerializeField] private PhysicsDisablerInteractable _physicsDisablerInteractable;

    public Transform Transform => GrabbableInteractable.Transform;
    public UnityEvent<IInteractionHandler> OnInteracted => GrabbableInteractable.OnInteracted;
    public UnityEvent<IInteractionHandler> OnFailedToInteract => GrabbableInteractable.OnFailedToInteract;

    public InteractionLayer InteractionLayer { get => GrabbableInteractable.InteractionLayer; set => GrabbableInteractable.InteractionLayer = value; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        if (!CanInteract(interactionHandler)) return false;
        StartCoroutine(InteractCoroutine(interactionHandler));
        return true;
    }

    public IEnumerator InteractCoroutine(IInteractionHandler interactionHandler)
    {
        if (!CanInteract(interactionHandler)) yield break;
        _ = _physicsDisablerInteractable == null || _physicsDisablerInteractable.Interact(interactionHandler);
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

    public bool CanInteract(IInteractionHandler interactionHandler)
    {
        return GrabbableInteractable.CanInteract(interactionHandler);
    }
}