using UnityEngine.Events;

public interface IInteractionRequester
{
    UnityEvent OnInteracted { get; }
    void Interact();
}