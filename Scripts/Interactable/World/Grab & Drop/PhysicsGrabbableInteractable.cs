using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsGrabbableInteractable : MonoBehaviour, IGrabbableInteractable
{
    private const float GRAB_DRAG_AUGMENT = 10.0f;
    private const float ACCEPTABLE_DISTANCE = 0.1f;

    [RequireInterface(typeof(IGrabbableInteractable))]
    [SerializeField] private Object _grabbableInteractableObject;
    private IGrabbableInteractable GrabbableInteractable => _grabbableInteractableObject as IGrabbableInteractable;

    [SerializeField] private bool _usePhysicsGrab = true;
    [SerializeField] [Range(0.0f, 1.0f)] private float _grabForceFactor = 0.15f;
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

        return (!_usePhysicsGrab
                && _physicsDisablerInteractable != null
                && _physicsDisablerInteractable.Interact(interactionHandler)
                && GrabbableInteractable.Interact(interactionHandler))
               || (_grabCoroutine = StartCoroutine(InteractCoroutine(interactionHandler))) != null;
    }

    public IEnumerator InteractCoroutine(IInteractionHandler interactionHandler)
    {
        if (!CanInteract(interactionHandler))
        {
            OnFailedToInteract?.Invoke(interactionHandler);
            yield break;
        }

        var grabber = interactionHandler as IGrabInteractionHandler;
        grabber.TryGetGrabParent(GrabbableInteractable, out var grabParent);

        yield return StartCoroutine(grabber.GrabInConstantTime ?
                                    GrabCoroutine(grabParent, 1.0f / grabber.GrabSpeed) :
                                    GrabCoroutine(grabParent, (t) => grabber.GrabSpeed));
        OnInteracted?.Invoke(interactionHandler);
    }           

    public IEnumerator GrabCoroutine(Transform grabberParent, System.Func<float, float> getGrabSpeed)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        // TODO - Move these to PhysicsEnabler/DisablerInteractable
        RigidbodyAccessor.UseGravity = false;
        RigidbodyAccessor.Constraints = RigidbodyConstraints.FreezeRotation;
        RigidbodyAccessor.Drag *= GRAB_DRAG_AUGMENT;

        Transform.SetParent(grabberParent);

        float initialDistance = GetDisplacement(grabberParent).magnitude;
        float distance = initialDistance;

        while (distance > ACCEPTABLE_DISTANCE)
        {
            const float THRESHOLD = 0.001f;
            distance = GetDisplacement(grabberParent).magnitude;
            float nT = 1.0f - distance / (initialDistance + THRESHOLD);
            var displacement = GetDisplacement(grabberParent);
            Vector3 force = GetForceToDisplaceOverTime(RigidbodyAccessor.Mass, displacement.normalized * initialDistance * Time.fixedDeltaTime, RigidbodyAccessor.Velocity - RigidbodyAccessor.Velocity.normalized * getGrabSpeed(nT), Time.fixedDeltaTime)
                            * _grabForceFactor;

            RigidbodyAccessor.AddForce(force);
            yield return waitForFixedUpdate;
        }

        yield return SustainGrab(grabberParent);
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, float grabTotalTime)
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

        RigidbodyAccessor.UseGravity = false;
        RigidbodyAccessor.Constraints = RigidbodyConstraints.FreezeRotation;
        RigidbodyAccessor.Drag *= GRAB_DRAG_AUGMENT;

        Transform.SetParent(grabberParent);

        float initialDistance = GetDisplacement(grabberParent).magnitude;

        for (float t = 0.0f; t < grabTotalTime; t += Time.fixedDeltaTime)
        {
            var displacement = GetDisplacement(grabberParent);
            Vector3 force = GetForceToDisplaceOverTime(RigidbodyAccessor.Mass, displacement.normalized * initialDistance, RigidbodyAccessor.Velocity, grabTotalTime)
                            * _grabForceFactor;

            RigidbodyAccessor.AddForce(force);
            yield return waitForFixedUpdate;
        }

        yield return SustainGrab(grabberParent);
    }

    private IEnumerator SustainGrab(Transform grabberParent)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        const int FRAMES_TO_SUSTAIN = 5;

        while (true)
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


    public void StopGrabbing()
    {
        if (_grabCoroutine == null) return;
        StopCoroutine(_grabCoroutine);
        _grabCoroutine = null;

        RigidbodyAccessor.UseGravity = true;
        RigidbodyAccessor.Constraints = RigidbodyConstraints.None;
        RigidbodyAccessor.Drag /= GRAB_DRAG_AUGMENT;
    }

    public bool CanInteract(IInteractionHandler interactionHandler)
    {
        return GrabbableInteractable.CanInteract(interactionHandler);
    }
}