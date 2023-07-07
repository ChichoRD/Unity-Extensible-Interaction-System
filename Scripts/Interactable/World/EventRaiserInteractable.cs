using UnityEngine;
using UnityEngine.Events;

public class EventRaiserInteractable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}
