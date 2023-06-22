using UnityEngine;

public interface IGrabberInteractor : IInteractor
{
    Transform GetGrabParent();
    float GrabSpeed { get; }
}