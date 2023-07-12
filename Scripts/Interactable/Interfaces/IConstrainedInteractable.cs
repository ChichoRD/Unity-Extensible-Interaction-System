using UnityEngine.Events;

public interface IConstrainedInteractable : IInteractable
{
    bool CanInteract(IInteractionHandler interactionHandler);
    UnityEvent<IInteractionHandler> OnFailedToInteract { get; }
}