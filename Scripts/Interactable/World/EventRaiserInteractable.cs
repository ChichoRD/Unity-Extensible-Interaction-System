using UnityEngine;
using UnityEngine.Events;

public class EventRaiserInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionLayer _interactionLayer = InteractionLayer.InteractionLayer0;
    public InteractionLayer InteractionLayer { get => _interactionLayer; set => _interactionLayer = value; }
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}
