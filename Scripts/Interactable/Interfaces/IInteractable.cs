using UnityEngine.Events;

public interface IInteractable
{
    void Interact(IInteractor interactor);
    UnityEvent<IInteractor> OnInteracted { get; }
}
