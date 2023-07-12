using UnityEngine;
using UnityEngine.Events;

public class PhysicsDroppableInteractable : MonoBehaviour, IDroppableInteractable
{
    [RequireInterface(typeof(IDroppableInteractable))]
    [SerializeField] private Object _droppableInteractableObject;
    private IDroppableInteractable DroppableInteractable => _droppableInteractableObject as IDroppableInteractable;

    [SerializeField] private PhysicsEnablerInteractable _physicsEnablerInteractable;

    public UnityEvent<IInteractionHandler> OnInteracted => DroppableInteractable.OnInteracted;
    public Transform Transform => DroppableInteractable.Transform;

    public InteractionLayer InteractionLayer { get => DroppableInteractable.InteractionLayer; set => DroppableInteractable.InteractionLayer = value; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        _ = _physicsEnablerInteractable == null || _physicsEnablerInteractable.Interact(interactionHandler);
        return DroppableInteractable.Interact(interactionHandler);
    }
}
