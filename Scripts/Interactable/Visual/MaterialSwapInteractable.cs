using UnityEngine;
using UnityEngine.Events;

public class MaterialSwapInteractable : MonoBehaviour, ISwitchableInteractable
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _material;
    private Material _originalMaterial;
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnInteracted { get; private set; }
    [field: SerializeField] public UnityEvent<IInteractionHandler> OnStoppedInteracting { get; private set; }

    public bool Interact(IInteractionHandler interactionHandler)
    {
        SwapToNewMaterial();
        OnInteracted?.Invoke(interactionHandler);
        return true;
    }

    public bool StopInteracting(IInteractionHandler interactionHandler)
    {
        SwapToOriginalMaterial();
        OnStoppedInteracting?.Invoke(interactionHandler);
        return true;
    }

    private void SwapToNewMaterial()
    {
        if (_renderer.sharedMaterial == _material) return;

        _originalMaterial = _renderer.material;
        _renderer.material = _material;
    }

    private void SwapToOriginalMaterial()
    {
        if (_renderer.sharedMaterial == _originalMaterial) return;

        _renderer.material = _originalMaterial;
    }
}
