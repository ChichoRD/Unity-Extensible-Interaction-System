using UnityEngine.Events;

public interface ISwitchableInteractable : IInteractable
{
    void StopInteracting(IInteractor interactor);
    UnityEvent<IInteractor> OnStoppedInteracting { get; }
}