using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractionRequester : MonoBehaviour, IInteractionRequester
{
    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    public IInteractor Interactor => _interactorObject as IInteractor;

    [SerializeField] private Object[] _interactionHandlerObjects;
    public IEnumerable<IInteractionHandler> InteractionHandlers => _interactionHandlerObjects.Cast<IInteractionHandler>();

    [field: SerializeField] public UnityEvent<IInteractable> OnInteracted { get; private set; }

    public void Interact()
    {
        var successfulInteractables = this.InteractAndGetSuccessful();
        foreach ((var interactable, var interactionHandler) in successfulInteractables)
        {
            OnInteracted?.Invoke(interactable);
            interactionHandler?.OnAcceptedForInteraction?.Invoke(interactable);
        }
    }
}
