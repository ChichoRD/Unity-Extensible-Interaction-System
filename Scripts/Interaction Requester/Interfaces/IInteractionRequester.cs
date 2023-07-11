using UnityEngine.Events;

public interface IInteractionRequester
{
    UnityEvent<IInteractable> OnInteracted { get; }
    void Interact();
}