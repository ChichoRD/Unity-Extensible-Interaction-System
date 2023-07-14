using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class DifferenceInteractor : MonoBehaviour, IUpdateableInteractor
{
    [SerializeField] private bool _listenForAddition = true;
    [SerializeField] private bool _listenForRemoval = true;

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
        Func<IEnumerable<IInteractable>> setDifference = (_listenForAddition, _listenForRemoval) switch
        {
            (_listenForAddition: true, _listenForRemoval: false) => () => _currentInteractables.Except(_previousInteractables),
            (_listenForAddition: false, _listenForRemoval: true) => () => _previousInteractables.Except(_currentInteractables),
            (_listenForAddition: true, _listenForRemoval: true) => () => _currentInteractables.Except(_previousInteractables).Union(_previousInteractables.Except(_currentInteractables)),
            _ => () => Enumerable.Empty<IInteractable>()
        };

        _differentInteractables.UnionWith(setDifference());
        return _differentInteractables.Count;
    }
}