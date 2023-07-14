using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstantInteractionUser : MonoBehaviour
{
    [RequireInterface(typeof(IInteractionRequester))]
    [SerializeField] private Object _interactorRequesterObject;
    private IInteractionRequester InteractionRequester => _interactorRequesterObject as IInteractionRequester;

    [SerializeField] private Object[] _updateableInteractorObjects;
    private IEnumerable<IUpdateableInteractor> UpdateableInteractors => _updateableInteractorObjects.Cast<IUpdateableInteractor>();

    private void Update()
    {
        foreach (var updateableInteractor in UpdateableInteractors)
            updateableInteractor?.UpdateInteractablesCollection();

        InteractionRequester.Interact();
    }
}