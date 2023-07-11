using UnityEngine;

public class InterchangeableGrabInteractionHandler : MonoBehaviour, IGrabInteractionHandler
{
    [SerializeField] private bool _interchangeOnGrabFail;
    [RequireInterface(typeof(IGrabInteractionHandler))]
    [SerializeField] private Object _grabInteractionHandlerObject;
    private IGrabInteractionHandler GrabInteractionHandler => _grabInteractionHandlerObject as IGrabInteractionHandler;
    private IInteractable _lastInteractable;

    public float GrabSpeed => GrabInteractionHandler.GrabSpeed;
    public bool GrabInConstantTime => GrabInteractionHandler.GrabInConstantTime;

    public bool TryGetGrabParent(IInteractable interactable, out Transform grabParent) => GrabInteractionHandler.TryGetGrabParent(interactable, out grabParent);
    public bool TryFreeGrabParent(IInteractable interactable, out Transform grabParent) => GrabInteractionHandler.TryFreeGrabParent(interactable, out grabParent)
                                                                                           && interactable is Component component
                                                                                           && component.gameObject.TryGetComponent(out IDroppableInteractable droppable)
                                                                                           && droppable.Interact(this);
    public bool TryAssignGrabParent(IInteractable interactable) => (GrabInteractionHandler.TryAssignGrabParent(interactable)
                                                                   && (_lastInteractable = interactable) != null)
                                                                   || (TryFreeGrabParent(_lastInteractable, out _)
                                                                   && TryAssignGrabParent(interactable));
}