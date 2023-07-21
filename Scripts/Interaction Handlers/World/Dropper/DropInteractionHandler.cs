using UnityEngine;
using UnityEngine.Events;

public class DropInteractionHandler : MonoBehaviour, IDropInteractionHandler
{
    [RequireInterface(typeof(IGrabInteractionHandler))]
    [SerializeField] private Object _grabInteractionHandlerObject;
    private IGrabInteractionHandler GrabInteractionHandler => _grabInteractionHandlerObject as IGrabInteractionHandler;
    [field: SerializeField] public UnityEvent<IInteractable> OnAcceptedForInteraction { get; private set; }

    public bool Drop(IInteractable interactable) => GrabInteractionHandler != null
                                                    && GrabInteractionHandler.TryFreeGrabParent(interactable, out _);
}