using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionGroupSetter : MonoBehaviour
{
    [SerializeField] private bool _setOnAwake = true;
    [SerializeField] private InteractionGroupObject _interactionGroupObject;
    [SerializeField] private Object[] _interactionHandlerObjects;
    private IEnumerable<IInteractionHandler> InteractionHandlers => _interactionHandlerObjects.Cast<IInteractionHandler>();
    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor => _interactorObject as IInteractor;

    private void Awake()
    {
        if (!_setOnAwake) return;
        TrySetInteractionGroup();
    }

    public bool TrySetInteractionGroup()
    {
        if (_interactionGroupObject == null) return false;

        _interactionGroupObject.SetInteractionHandlers(InteractionHandlers);
        _interactionGroupObject.SetInteractor(Interactor);
        return true;
    }
}
