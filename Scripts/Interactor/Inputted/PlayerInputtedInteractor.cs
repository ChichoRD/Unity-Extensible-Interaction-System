using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputtedInteractor : MonoBehaviour
{
    [SerializeField] private InputActionReference _interactAction;

    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor => _interactorObject as IInteractor;

    private void Awake()
    {
        _interactAction.action.performed += OnInteractActionPerformed;
    }

    private void OnEnable()
    {
        _interactAction.action.Enable();
    }

    private void OnDisable()
    {
        _interactAction.action.Disable();
    }

    private void OnDestroy()
    {
        _interactAction.action.performed -= OnInteractActionPerformed;
    }

    private void OnInteractActionPerformed(InputAction.CallbackContext context)
    {
        var interactables = Interactor.GetInteractables();
        foreach (var interactable in interactables)
            interactable.Interact(Interactor);
    }
}
