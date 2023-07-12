using System.Collections.Generic;
using UnityEngine.Events;

public interface IInteractionRequester
{
    IInteractor Interactor { get; }
    IEnumerable<IInteractionHandler> InteractionHandlers { get; }
    UnityEvent<IInteractable> OnInteracted { get; }
    void Interact();
}