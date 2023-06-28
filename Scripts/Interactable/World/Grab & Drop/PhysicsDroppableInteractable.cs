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
    public UnityEvent<IInteractor> OnInteracted => DroppableInteractable.OnInteracted;

    public void Interact(IInteractor interactor)
    {
        RigidbodyAccessor.IsKinematic = false;
        DroppableInteractable.Interact(interactor);
    }

    public void Drop()
    {
        RigidbodyAccessor.IsKinematic = false;
        DroppableInteractable.Drop();
    }
}