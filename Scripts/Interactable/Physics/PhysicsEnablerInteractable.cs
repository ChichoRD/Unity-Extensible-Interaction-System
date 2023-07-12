using UnityEngine;
using UnityEngine.Events;

public class PhysicsEnablerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionLayer _interactionLayer = InteractionLayer.InteractionLayer0;
    public InteractionLayer InteractionLayer { get => _interactionLayer; set => _interactionLayer = value; }

    [SerializeField] private bool _affectPhysics = true;
    [SerializeField] private bool _affectCollision = true;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        RigidbodyAccessor.IsKinematic = !_affectPhysics;

        if (_affectCollision)
            RigidbodyAccessor.EnableRigidbodyCollisions();

        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}