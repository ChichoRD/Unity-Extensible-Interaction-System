using UnityEngine.Events;

public interface IInteractable
{
    InteractionLayer InteractionLayer { get; set; }
    bool Interact(IInteractionHandler interactionHandler);
    UnityEvent<IInteractionHandler> OnInteracted { get; }
}
