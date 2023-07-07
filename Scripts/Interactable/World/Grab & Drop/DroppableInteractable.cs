using UnityEngine;
using UnityEngine.Events;

public class DroppableInteractable : MonoBehaviour, IDroppableInteractable
{
    [SerializeField] private Transform _transform;
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        Drop();

        if (interactionHandler is IGrabInteractionHandler grabber)
            grabber.FreeGrabParent(this, out _);

        OnInteracted?.Invoke(interactionHandler);
        return true;
    }

    public void Drop()
    {
        _transform.SetParent(null);
    }
}
