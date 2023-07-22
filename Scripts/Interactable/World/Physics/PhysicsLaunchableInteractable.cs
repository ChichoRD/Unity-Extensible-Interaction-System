using UnityEngine;
using UnityEngine.Events;

public class PhysicsLaunchableInteractable : MonoBehaviour, IInteractable
{
    [RequireInterface(typeof(IRigidbodyAccessor))]
    [SerializeField] private Object _rigidbodyAccessorObject;
    private IRigidbodyAccessor RigidbodyAccessor => _rigidbodyAccessorObject as IRigidbodyAccessor;

    [field: SerializeField] public InteractionLayer InteractionLayer { get; set; } = InteractionLayer.InteractionLayer0;
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; set; }
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnFailedToInteract { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        if (!CanInteract(interactionHandler))
        {
            OnFailedToInteract?.Invoke(interactionHandler);
            return false;
        }

        var launchHandler = interactionHandler as IPhysicsLaunchInteractionHandler;
        RigidbodyAccessor.AddImpulse(launchHandler.LaunchImpulse);
        OnInteracted?.Invoke(interactionHandler);
        return true;
    }

    public bool CanInteract(IInteractionHandler interactionHandler) => interactionHandler is IPhysicsLaunchInteractionHandler;
}