using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class CyclicInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionLayer _interactionLayer = InteractionLayer.InteractionLayer0;
    public InteractionLayer InteractionLayer { get => _interactionLayer; set => _interactionLayer = value; }

    [Serializable]
    private struct InteractableData
    {
        [RequireInterface(typeof(IInteractable))]
        [SerializeField] private Object _interactableObject;
        public readonly IInteractable Interactable => _interactableObject as IInteractable;
        [SerializeField] private bool _requireSuccessToContinueCycling;
        public readonly bool RequireSuccessToContinueCycling => _requireSuccessToContinueCycling;
    }

    [SerializeField] private InteractableData[] _interactables;
    private int _currentInteractableIndex;
    private InteractableData CurrentInteractable => _interactables[_currentInteractableIndex];

    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        bool success = CurrentInteractable.Interactable.Interact(interactionHandler);
        bool cycle = success || !CurrentInteractable.RequireSuccessToContinueCycling;
        _currentInteractableIndex = cycle ? ++_currentInteractableIndex % _interactables.Length : _currentInteractableIndex;
        if (success)
            OnInteracted?.Invoke(interactionHandler);
        return success;
    }
}