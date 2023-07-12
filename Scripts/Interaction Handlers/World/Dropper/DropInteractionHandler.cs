using UnityEngine;

public class DropInteractionHandler : MonoBehaviour, IDropInteractionHandler
{
    [RequireInterface(typeof(IGrabInteractionHandler))]
    [SerializeField] private Object _grabInteractionHandlerObject;
    private IGrabInteractionHandler GrabInteractionHandler => _grabInteractionHandlerObject as IGrabInteractionHandler;

    public bool Drop(IInteractable interactable) => GrabInteractionHandler != null
                                                    && GrabInteractionHandler.TryFreeGrabParent(interactable, out _);
}