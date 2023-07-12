using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegisterInteractor : MonoBehaviour, IInteractor
{
    [SerializeField] private bool _performComponentSearch;
    [RequireInterface(typeof(IInteractionRequester))]
    [SerializeField] private Object _interactionRequesterObject;
    private IInteractionRequester InteractionRequester => _interactionRequesterObject as IInteractionRequester;

    private readonly List<IInteractable> _register = new List<IInteractable>();

    private void Awake() => InteractionRequester?.OnInteracted.AddListener(OnInteracted);

    private void OnDestroy() => InteractionRequester?.OnInteracted.RemoveListener(OnInteracted);

    private void OnInteracted(IInteractable arg0) => _register.Add(arg0);
    private IEnumerable<IInteractable> GetAllIncludedInteractables(IInteractable interactable) => interactable is Component c ? c.GetComponents<IInteractable>() : interactable.Yield();
    public IEnumerable<IInteractable> GetInteractables() => _performComponentSearch
                                                            ? _register.SelectMany(i => GetAllIncludedInteractables(i))
                                                            : _register;

    public void ClearRegister() => _register.Clear();
    public void RemoveFromRegister(IInteractable interactable) => _ = _performComponentSearch
                                                                      ? _register.RemoveAll(GetAllIncludedInteractables(interactable).Contains) > 0
                                                                      : _register.Remove(interactable);
    public int RemoveFromRegister(IEnumerable<IInteractable> interactables) => _performComponentSearch
                                                                               ? _register.RemoveAll(i => GetAllIncludedInteractables(i).Any(interactables.Contains))
                                                                               : _register.RemoveAll(interactables.Contains);
    public int RemoveFromRegister(params IInteractable[] interactables) => RemoveFromRegister(interactables.AsEnumerable());
    public void AddToRegister(IInteractable interactable) => _register.Add(interactable);
    public void AddToRegister(IEnumerable<IInteractable> interactables) => _register.AddRange(interactables);
    public void AddToRegister(params IInteractable[] interactables) => _register.AddRange(interactables);
}
