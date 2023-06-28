using UnityEngine;

public interface IGrabberInteractor : IInteractor
{
    Transform GetGrabParent(IInteractable interactable);
    bool FreeGrabParent(IInteractable interactable, out Transform grabParent);
    float GrabSpeed { get; }
    bool GrabInConstantTime { get; }
}
