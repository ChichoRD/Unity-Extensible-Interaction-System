using UnityEngine;
using UnityEngine.Events;

public class DroppableInteractable : MonoBehaviour, IDroppableInteractable
{
    [SerializeField] private Transform _transform;
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public Transform Transform => _transform;

    public bool Interact(IInteractionHandler interactionHandler)
    {
        Transform.SetParent(null);
        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}
