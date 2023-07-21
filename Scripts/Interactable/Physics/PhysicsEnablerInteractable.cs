using UnityEngine;
using UnityEngine.Events;

public class PhysicsEnablerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionLayer _interactionLayer = InteractionLayer.InteractionLayer0;
    public InteractionLayer InteractionLayer { get => _interactionLayer; set => _interactionLayer = value; }

    [SerializeField] private bool _affectPhysics = true;
    [SerializeField] private bool _affectGravity = true;
    [SerializeField] private bool _affectCollision = true;

    [SerializeField] private bool _affectDrag = true;
    [SerializeField] [Min(0.0f)] private float _drag = 0.0f;
    [SerializeField] private bool _affectAngularDrag = false;
    [SerializeField] [Min(0.0f)] private float _angularDrag = 0.05f;

    [SerializeField] private RigidbodyConstraints _affectedConstraints = RigidbodyConstraints.None;

    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        RigidbodyAccessor.IsKinematic &= !_affectPhysics;
        RigidbodyAccessor.UseGravity |= _affectGravity;

        RigidbodyAccessor.Drag = _affectDrag ? _drag : RigidbodyAccessor.Drag;
        RigidbodyAccessor.AngularDrag = _affectAngularDrag ? _angularDrag : RigidbodyAccessor.AngularDrag;

        RigidbodyAccessor.Constraints &= ~_affectedConstraints;

        if (_affectCollision)
            RigidbodyAccessor.EnableRigidbodyCollisions();

        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}