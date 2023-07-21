using System.Collections.Generic;
using System.Linq;

public static class InteractionRequesterExtensions
{
    public static IEnumerable<IInteractable> InteractAndGetSuccessful(this IInteractionRequester interactionRequester)
    {
        var interactables = interactionRequester.Interactor.GetInteractables();
        var handlers = interactionRequester.InteractionHandlers.Prepend(null);
        List<IInteractable> successfulInteractables = new List<IInteractable>();

        foreach (var interactable in interactables)
        {
            foreach (var interactionHandler in handlers)
            {
                if (interactable.Interact(interactionHandler))
                {
                    successfulInteractables.Add(interactable);
                    interactionHandler?.OnAcceptedForInteraction?.Invoke(interactable);
                    break;
                }
            }
        }

        return successfulInteractables;
    }
}