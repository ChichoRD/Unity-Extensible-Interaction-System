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

    [field: SerializeField] public UnityEvent<IInteractable> OnInteracted { get; private set; }

    public void Interact()
    {
        var interactables = Interactor.GetInteractables();
        var handlers = InteractionHandlers.Append(null);
        List<IInteractable> successfulInteractables = new List<IInteractable>();

        foreach (var interactable in interactables)
        {
            foreach (var interactionHandler in handlers)
            {
                if (interactable.Interact(interactionHandler))
                {
                    successfulInteractables.Add(interactable);
                    break;
                }
            }
        }

        foreach (var interactable in successfulInteractables)
            OnInteracted?.Invoke(interactable);
    }
}