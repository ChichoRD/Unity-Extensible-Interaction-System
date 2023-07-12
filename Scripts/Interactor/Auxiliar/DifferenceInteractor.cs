using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DifferenceInteractor : MonoBehaviour, IUpdateableInteractor
{
    [SerializeField] private bool _listenForAddition;
    [SerializeField] private bool _listenForRemoval;

    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor  => _interactorObject as IInteractor;
    private readonly HashSet<IInteractable> _differentInteractables = new HashSet<IInteractable>();
    private readonly HashSet<IInteractable> _currentInteractables = new HashSet<IInteractable>();
    private readonly HashSet<IInteractable> _previousInteractables = new HashSet<IInteractable>();

    public IEnumerable<IInteractable> GetInteractables() => _differentInteractables;

    public IEnumerable<ISwitchableInteractable> GetSwitchableInteractables() => _differentInteractables.Where(interactable => interactable is ISwitchableInteractable).Cast<ISwitchableInteractable>();

    public int UpdateInteractablesCollection()
    {
        _previousInteractables.Clear();
        _previousInteractables.UnionWith(_currentInteractables);

        _currentInteractables.Clear();
        _currentInteractables.UnionWith(Interactor.GetInteractables());

        _differentInteractables.Clear();
        if (_listenForAddition)
        {
            _differentInteractables.UnionWith(_currentInteractables);
            _differentInteractables.ExceptWith(_previousInteractables);
        }

        if (_listenForRemoval)
        {
            _differentInteractables.UnionWith(_previousInteractables);
            _differentInteractables.ExceptWith(_currentInteractables);
        }
        return _currentInteractables.Count;
    }
}