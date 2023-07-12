using UnityEngine;
using UnityEngine.Events;

public class DroppableInteractable : MonoBehaviour, IDroppableInteractable
{
    [SerializeField] private InteractionLayer _interactionLayer = InteractionLayer.InteractionLayer0;
    public InteractionLayer InteractionLayer { get => _interactionLayer; set => _interactionLayer = value; }

    [SerializeField] private Transform _transform;

    [RequireInterface(typeof(IGrabbableInteractable))]
    [SerializeField] private Object _grabbableInteractableObject;
    public IGrabbableInteractable GrabbableInteractable => _grabbableInteractableObject as IGrabbableInteractable;

    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public Transform Transform => _transform;

    public bool Interact(IInteractionHandler interactionHandler)
    {
        Transform.SetParent(null);
        if (interactionHandler is IDropInteractionHandler dropper)
            dropper.Drop(GrabbableInteractable);

        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}
