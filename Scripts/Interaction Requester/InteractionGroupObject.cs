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
    public void Interact()
    {
        var interactables = _interactor.GetInteractables();
        var handlers = _interactionHandlers.Append(null);
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
