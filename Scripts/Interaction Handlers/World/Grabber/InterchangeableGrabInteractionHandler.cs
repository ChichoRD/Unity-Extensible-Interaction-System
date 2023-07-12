using System;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public bool TryFreeGrabParent(IInteractable interactable, out Transform grabParent) => (grabParent = null) == null
                                                                                           && interactable is Component component
                                                                                           && new Func<bool>(() =>
                                                                                           {
                                                                                               var droppable = component.GetComponentInParent<IDroppableInteractable>();
                                                                                               return droppable != null
                                                                                               && droppable.Interact(this)
                                                                                               && GrabInteractionHandler.TryFreeGrabParent(interactable, out _);
                                                                                           })();
                                                                                           
    public bool TryAssignGrabParent(IInteractable interactable) => (GrabInteractionHandler.TryAssignGrabParent(interactable)
                                                                    && (_lastInteractable = interactable) != null)
                                                                   || (TryFreeGrabParent(_lastInteractable, out _)
                                                                       && TryAssignGrabParent(interactable));
}