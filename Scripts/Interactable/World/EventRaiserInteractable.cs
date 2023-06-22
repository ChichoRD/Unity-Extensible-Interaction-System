using UnityEngine;
using UnityEngine.Events;

public class EventRaiserInteractable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public UnityEvent<IInteractor> OnInteracted { get; private set; }

    public void Interact(IInteractor interactor) => OnInteracted?.Invoke(interactor);
}
