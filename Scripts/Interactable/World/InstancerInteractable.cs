using UnityEngine;
using UnityEngine.Events;

public class InstancerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionLayer _interactionLayer;
    public InteractionLayer InteractionLayer { get => _interactionLayer; set => _interactionLayer = value; }

    [SerializeField] private Object _prefab;
    [SerializeField] private bool _parentInstance;
    [SerializeField] private Transform _instantiationTransform;
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        Instantiate(_prefab, _instantiationTransform.position, _instantiationTransform.rotation, _parentInstance ? _instantiationTransform : null);
        OnInteracted?.Invoke(interactionHandler);
        return true;
    }
}
