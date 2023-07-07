using UnityEngine;

public class InteractionHandler : MonoBehaviour, IInteractionHandler
{
    public bool OnInteractionRequest(IInteractable interactable) => interactable.Interact(this);
}