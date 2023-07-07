using UnityEngine.Events;

public interface IInteractable
{
    bool Interact(IInteractionHandler interactionHandler);
    UnityEvent<IInteractionHandler> OnInteracted { get; }
}
