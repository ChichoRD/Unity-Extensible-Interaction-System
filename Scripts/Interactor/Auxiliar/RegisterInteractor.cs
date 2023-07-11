using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegisterInteractor : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(IInteractionRequester))]
    [SerializeField] private Object _interactionRequesterObject;
    private IInteractionRequester InteractionRequester => _interactionRequesterObject as IInteractionRequester;

    [SerializeReference] private /*readonly*/ List<IInteractable> _register = new List<IInteractable>();

    private void Awake() => InteractionRequester?.OnInteracted.AddListener(OnInteracted);

    private void OnDestroy() => InteractionRequester?.OnInteracted.RemoveListener(OnInteracted);

    private void OnInteracted(IInteractable arg0) => _register.Add(arg0);

    public IEnumerable<IInteractable> GetInteractables() => _register;

    public void ClearRegister() => _register.Clear();
    public void RemoveFromRegister(IInteractable interactable) => _register.Remove(interactable);
    public int RemoveFromRegister(IEnumerable<IInteractable> interactables) => _register.RemoveAll(interactables.Contains);
    public int RemoveFromRegister(params IInteractable[] interactables) => RemoveFromRegister(interactables.AsEnumerable());
    public void AddToRegister(IInteractable interactable) => _register.Add(interactable);
    public void AddToRegister(IEnumerable<IInteractable> interactables) => _register.AddRange(interactables);
    public void AddToRegister(params IInteractable[] interactables) => _register.AddRange(interactables);
}