using System;
using UnityEngine.Events;

[Obsolete]
public interface ISwitchableInteractable : IInteractable
{
    bool StopInteracting(IInteractionHandler interactionHandler);
    UnityEvent<IInteractionHandler> OnStoppedInteracting { get; }
}