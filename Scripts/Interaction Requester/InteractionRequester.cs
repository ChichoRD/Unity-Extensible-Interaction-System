using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractionRequester : MonoBehaviour, IInteractionRequester
{
    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor => _interactorObject as IInteractor;

    [SerializeField] private Object[] _interactionHandlerObjects;
    private IEnumerable<IInteractionHandler> InteractionHandlers => _interactionHandlerObjects.Cast<IInteractionHandler>();

    [field: SerializeField] public UnityEvent OnInteracted { get; private set; }

    public void Interact()
    {
        var interactables = Interactor.GetInteractables();
        foreach (var interactionHandler in InteractionHandlers)
            foreach (var interactable in interactables)
                interactionHandler.OnInteractionRequest(interactable);
        OnInteracted?.Invoke();
    }
}