using UnityEngine.Events;

public interface IConstrainedInteractable : IInteractable
{
    IInteractionConstrainer InteractionConstrainer { get; }
    UnityEvent<IInteractor> OnFailedToInteract { get; }
}
