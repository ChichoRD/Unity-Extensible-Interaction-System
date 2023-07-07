using UnityEngine.Events;

public interface ISwitchableInteractable : IInteractable
{
    bool StopInteracting(IInteractionHandler interactionHandler);
    UnityEvent<IInteractionHandler> OnStoppedInteracting { get; }
}