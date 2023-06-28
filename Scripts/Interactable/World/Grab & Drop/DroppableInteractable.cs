using UnityEngine;
using UnityEngine.Events;

public class DroppableInteractable : MonoBehaviour, IDroppableInteractable
{
    [SerializeField] private Transform _transform;
    [field: SerializeField] public UnityEvent<IInteractor> OnInteracted { get; private set; }

    public void Interact(IInteractor interactor)
    {
        Drop();

        if (interactor is IGrabberInteractor grabber)
            grabber.FreeGrabParent(this, out _);

        OnInteracted?.Invoke(interactor);
    }

    public void Drop()
    {
        _transform.SetParent(null);
    }
}
