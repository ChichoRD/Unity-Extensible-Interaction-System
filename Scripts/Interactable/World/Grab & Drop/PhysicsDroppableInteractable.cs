using UnityEngine;
using UnityEngine.Events;

public class PhysicsDroppableInteractable : MonoBehaviour, IDroppableInteractable
{
    [SerializeField] private bool _enablePhysicsOnDrop = true;
    [SerializeField] private bool _enableCollisionOnDrop = true;

    [RequireInterface(typeof(IDroppableInteractable))]
    [SerializeField] private Object _droppableInteractableObject;
    private IDroppableInteractable DroppableInteractable => _droppableInteractableObject as IDroppableInteractable;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    public UnityEvent<IInteractionHandler> OnInteracted => DroppableInteractable.OnInteracted;
    public Transform Transform => DroppableInteractable.Transform;

    public bool Interact(IInteractionHandler interactionHandler)
    {
        RigidbodyAccessor.IsKinematic = !_enablePhysicsOnDrop;
        if (_enableCollisionOnDrop)
            RigidbodyAccessor.EnableRigidbodyCollisions();
        return DroppableInteractable.Interact(interactionHandler);
    }
}