using System.Collections.Generic;
using System.Linq;

public static class InteractionRequesterExtensions
{
    public static IEnumerable<(IInteractable, IInteractionHandler)> InteractAndGetSuccessful(this IInteractionRequester interactionRequester)
    {
        var interactables = interactionRequester.Interactor.GetInteractables();
        var handlers = interactionRequester.InteractionHandlers.Prepend(null);
        var successfulInteractables = new List<(IInteractable, IInteractionHandler)>();

        foreach (var interactable in interactables)
        {
            foreach (var interactionHandler in handlers)
            {
                if (interactable.Interact(interactionHandler))
                {
                    successfulInteractables.Add((interactable, interactionHandler));
                    break;
                }
            }
        }

        return successfulInteractables;
    }
}