using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsGrabbableInteractable : MonoBehaviour, IGrabbableInteractable
{
    private const float ACCEPTABLE_DISTANCE = 0.1f;

    [RequireInterface(typeof(IGrabbableInteractable))]
    [SerializeField] private Object _grabbableInteractableObject;
    private IGrabbableInteractable GrabbableInteractable => _grabbableInteractableObject as IGrabbableInteractable;

    [SerializeField] private bool _usePhysicsGrab = true;
    [SerializeField] [Range(0.0f, 1.0f)] private float _grabForceFactor = 0.95f;
    [SerializeField] private PhysicsDisablerInteractable _physicsDisablerInteractable;
    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;
    private Coroutine _grabCoroutine;

    public Transform Transform => GrabbableInteractable.Transform;
    public UnityEvent<IInteractionHandler> OnInteracted => GrabbableInteractable.OnInteracted;
    public UnityEvent<IInteractionHandler> OnFailedToInteract => GrabbableInteractable.OnFailedToInteract;

    public InteractionLayer InteractionLayer { get => GrabbableInteractable.InteractionLayer; set => GrabbableInteractable.InteractionLayer = value; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        if (!CanInteract(interactionHandler))
        {
            OnFailedToInteract?.Invoke(interactionHandler);
            return false;
        }

        return (_physicsDisablerInteractable != null
                && _physicsDisablerInteractable.Interact(interactionHandler))
                && (_grabCoroutine ??= StartCoroutine(InteractCoroutine(interactionHandler))) != null;
    }

    public IEnumerator InteractCoroutine(IInteractionHandler interactionHandler)
    {
        if (!CanInteract(interactionHandler))
        {
            OnFailedToInteract?.Invoke(interactionHandler);
            _grabCoroutine = null;
            yield break;
        }

        yield return GrabbableInteractable.InteractCoroutine(interactionHandler);
        if (!_usePhysicsGrab)
        {
            _grabCoroutine = null;
            yield break;
        }

        var grabber = interactionHandler as IGrabInteractionHandler;
        grabber.TryGetGrabParent(GrabbableInteractable, out var grabParent);
        yield return SustainGrabCoroutine(grabParent);
    }           

    public IEnumerator GrabCoroutine(Transform grabberParent, System.Func<float, float> getGrabSpeed)
    {
        yield return GrabbableInteractable.GrabCoroutine(grabberParent, getGrabSpeed);
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, float grabTotalTime)
    {
        yield return GrabbableInteractable.GrabCoroutine(grabberParent, grabTotalTime);
    }

    private IEnumerator SustainGrabCoroutine(Transform grabberParent)
    {
        // v * dt = dx
        // F * dt = m * dv
        // F = m * dv / dt

        // Change in v should yield a new v that sustained during dt equals dx
        // (v + dv) * dt = dx
        // v * dt + dv * dt = dx
        // dv = (dx - v * dt) / dt

        // F * dt = m * (dx - v * dt) / dt
        // F = m * (dx - v * dt) / (dt * dt)
        // dv = (dx - v * dt) / dt

        // If dv = 0
        // dx - v * dt = 0
        // dx = v * dt

        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        const int FRAMES_TO_SUSTAIN = 5;

        while (RigidbodyAccessor != null)
        {
            var displacement = GetDisplacement(grabberParent);
            Vector3 force = GetForceToDisplaceOverTime(RigidbodyAccessor.Mass, Step(ACCEPTABLE_DISTANCE, displacement.magnitude) * displacement, RigidbodyAccessor.Velocity, Time.fixedDeltaTime * FRAMES_TO_SUSTAIN)
                            * _grabForceFactor;

            RigidbodyAccessor.AddForce(force);
            yield return waitForFixedUpdate;
        }
    }

    private Vector3 GetDisplacement(Transform grabberParent) => grabberParent.position - Transform.position;
    private float Step(float s, float x) => s > x ? 0.0f : 1.0f;
    private Vector3 GetForceToDisplaceOverTime(float m, Vector3 dx, Vector3 v, float dt) => m * (dx - v * dt) / (dt * dt);

    public void StopGrabCoroutine()
    {
        if (_grabCoroutine == null) return;
        StopCoroutine(_grabCoroutine);
        _grabCoroutine = null;
    }

    public bool CanInteract(IInteractionHandler interactionHandler)
    {
        return GrabbableInteractable.CanInteract(interactionHandler);
    }
}