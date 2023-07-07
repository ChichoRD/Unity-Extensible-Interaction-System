using UnityEngine;

public class InterchangeableGrabInteractionHandler : MonoBehaviour, IGrabInteractionHandler
{
    [SerializeField] private bool _interchangeOnGrabFail;
    [RequireInterface(typeof(IGrabInteractionHandler))]
    [SerializeField] private Object _grabInteractionHandlerObject;
    private IGrabInteractionHandler GrabInteractionHandler => _grabInteractionHandlerObject as IGrabInteractionHandler;

    public float GrabSpeed => GrabInteractionHandler.GrabSpeed;
    public bool GrabInConstantTime => GrabInteractionHandler.GrabInConstantTime;

    public bool FreeGrabParent(IInteractable interactable, out Transform grabParent) => GrabInteractionHandler.FreeGrabParent(interactable, out grabParent);

    public Transform GetGrabParent(IInteractable interactable) => GrabInteractionHandler.GetGrabParent(interactable);

    public bool OnInteractionRequest(IInteractable interactable) => GrabInteractionHandler.OnInteractionRequest(interactable) ||
                                                                        (_interchangeOnGrabFail &&
                                                                        FreeGrabParent(interactable /*Last added, impl*/, out _) &&
                                                                        GrabInteractionHandler.OnInteractionRequest(interactable));
}