using UnityEngine;

public interface IGrabInteractionHandler : IInteractionHandler
{
    float GrabSpeed { get; }
    bool GrabInConstantTime { get; }
    Transform GetGrabParent(IInteractable interactable);
    bool FreeGrabParent(IInteractable interactable, out Transform grabParent);
}