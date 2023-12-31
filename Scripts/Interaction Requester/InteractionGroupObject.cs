using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = FILE_NAME, menuName = MENU_NAME)]
public class InteractionGroupObject : ScriptableObject, IInteractionRequester
{
    public const string FILE_NAME = "Interaction Group";
    public const string MENU_NAME = "Interaction System/" + FILE_NAME;

    private IInteractor _interactor;
    private readonly List<IInteractionHandler> _interactionHandlers = new List<IInteractionHandler>();

    [field: SerializeField] public UnityEvent<IInteractable> OnInteracted { get; private set; }

    public IInteractor Interactor => _interactor;

    public IEnumerable<IInteractionHandler> InteractionHandlers => _interactionHandlers;

    public void Interact()
    {
        var successfulInteractables = this.InteractAndGetSuccessful();
        foreach ((var interactable, var interactionHandler) in successfulInteractables)
        {
            OnInteracted?.Invoke(interactable);
            interactionHandler?.OnAcceptedForInteraction?.Invoke(interactable);
        }
    }

    public void AddInteractionHandlers(params IInteractionHandler[] interactionHandlers) => _interactionHandlers.AddRange(interactionHandlers);
    public void AddInteractionHandlers(IEnumerable<IInteractionHandler> interactionHandlers) => _interactionHandlers.AddRange(interactionHandlers);

    public void RemoveInteractionHandlers(params IInteractionHandler[] interactionHandlers) => RemoveInteractionHandlers(interactionHandlers.AsEnumerable());
    public void RemoveInteractionHandlers(IEnumerable<IInteractionHandler> interactionHandlers) => _interactionHandlers.RemoveAll(interactionHandlers.Contains);

    public void SetInteractionHandlers(params IInteractionHandler[] interactionHandlers)
    {
        _interactionHandlers.Clear();
        AddInteractionHandlers(interactionHandlers);
    }

    public void SetInteractionHandlers(IEnumerable<IInteractionHandler> interactionHandlers)
    {
        _interactionHandlers.Clear();
        AddInteractionHandlers(interactionHandlers);
    }

    public void SetInteractor(IInteractor interactor) => _interactor = interactor;
}
