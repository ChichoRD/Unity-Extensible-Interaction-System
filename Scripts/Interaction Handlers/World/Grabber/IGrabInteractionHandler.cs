using UnityEngine;

public interface IGrabInteractionHandler : IInteractionHandler
{
    float GrabSpeed { get; }
    bool GrabInConstantTime { get; }
    bool TryGetGrabParent(IInteractable interactable, out Transform grabParent);
    bool TryAssignGrabParent(IInteractable interactable);
    bool TryFreeGrabParent(IInteractable interactable, out Transform grabParent);
}