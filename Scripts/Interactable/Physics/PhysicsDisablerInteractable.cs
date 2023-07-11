using UnityEngine;
using UnityEngine.Events;

public class PhysicsDisablerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _affectPhysics = true;
    [SerializeField] private bool _affectCollision = true;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        RigidbodyAccessor.IsKinematic = _affectPhysics;

        if (_affectCollision)
            RigidbodyAccessor.DisableRigidbodyCollisions();

        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}
