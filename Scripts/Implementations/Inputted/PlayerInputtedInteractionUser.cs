using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInputtedInteractionUser : MonoBehaviour
{
#if ENABLE_INPUT_SYSTEM
    [SerializeField] private InputActionReference _interactAction;

    [RequireInterface(typeof(IInteractionRequester))]
    [SerializeField] private Object _interactorRequesterObject;
    private IInteractionRequester InteractionRequester => _interactorRequesterObject as IInteractionRequester;

    [RequireInterface(typeof(IUpdateableInteractor))]
    [SerializeField] private Object _updateableInteractorObject;
    private IUpdateableInteractor UpdateableInteractor => _updateableInteractorObject as IUpdateableInteractor;


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

    private void Update()
    {
        UpdateableInteractor?.UpdateInteractablesCollection();
    }

    private void OnInteractActionPerformed(InputAction.CallbackContext context)
    {
        InteractionRequester.Interact();
    }
#endif
}
