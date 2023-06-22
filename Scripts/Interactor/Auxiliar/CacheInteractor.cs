using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CacheInteractor : MonoBehaviour, IInteractor
{
    private IEnumerable<IInteractable> _interactables = new IInteractable[0];

    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor => _interactorObject as IInteractor;

    public IEnumerable<IInteractable> GetInteractables() => _interactables;
    public int SetInteractables() => (_interactables = Interactor.GetInteractables()).Count();
}
