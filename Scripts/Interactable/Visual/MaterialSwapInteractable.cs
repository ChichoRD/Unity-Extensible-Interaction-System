using UnityEngine;
using UnityEngine.Events;

public class MaterialSwapInteractable : MonoBehaviour, ISwitchableInteractable
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _material;
    private Material _originalMaterial;
    [field: SerializeField] public UnityEvent<IInteractor> OnInteracted { get; private set; }
    [field: SerializeField] public UnityEvent<IInteractor> OnStoppedInteracting { get; private set; }

    public void Interact(IInteractor interactor)
    {
        SwapToNewMaterial();
        OnInteracted?.Invoke(interactor);
    }

    public void StopInteracting(IInteractor interactor)
    {
        SwapToOriginalMaterial();
        OnStoppedInteracting?.Invoke(interactor);
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
