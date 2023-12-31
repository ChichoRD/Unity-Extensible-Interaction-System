using System.Collections.Generic;
using System.Linq;
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

    
    [SerializeField] private Object[] _updateableInteractorObjects;
    private IEnumerable<IUpdateableInteractor> UpdateableInteractors => _updateableInteractorObjects.Cast<IUpdateableInteractor>();


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
        foreach (var updateableInteractor in UpdateableInteractors)
            updateableInteractor?.UpdateInteractablesCollection();
    }

    private void OnInteractActionPerformed(InputAction.CallbackContext context)
    {
        InteractionRequester.Interact();
    }
#endif
}
