using UnityEngine;
using UnityEngine.Events;

public class PhysicsDroppableInteractable : MonoBehaviour, IDroppableInteractable
{
    [RequireInterface(typeof(IDroppableInteractable))]
    [SerializeField] private Object _droppableInteractableObject;
    private IDroppableInteractable DroppableInteractable => _droppableInteractableObject as IDroppableInteractable;
    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;
    public UnityEvent<IInteractionHandler> OnInteracted => DroppableInteractable.OnInteracted;

    public bool Interact(IInteractionHandler interactionHandler)
    {
        RigidbodyAccessor.IsKinematic = false;
        return DroppableInteractable.Interact(interactionHandler);
    }

    public void Drop()
    {
        RigidbodyAccessor.IsKinematic = false;
        DroppableInteractable.Drop();
    }
}