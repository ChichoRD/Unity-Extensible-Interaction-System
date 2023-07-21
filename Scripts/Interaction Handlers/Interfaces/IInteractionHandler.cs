using UnityEngine.Events;

public interface IInteractionHandler
{
    UnityEvent<IInteractable> OnAcceptedForInteraction { get; }
}