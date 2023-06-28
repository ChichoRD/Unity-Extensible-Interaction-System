using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GrabbableDroppableInteractable : MonoBehaviour, IGrabbableInteractable, IDroppableInteractable
{
    [RequireInterface(typeof(IGrabbableInteractable))]
    [SerializeField] private Object _grabbableInteractableObject;
    private IGrabbableInteractable GrabbableInteractable => _grabbableInteractableObject as IGrabbableInteractable;

    [RequireInterface(typeof(IDroppableInteractable))]
    [SerializeField] private Object _droppableInteractableObject;
    private IDroppableInteractable DroppableInteractable => _droppableInteractableObject as IDroppableInteractable;

    private readonly IInteractable[] _interactables = new IInteractable[2];
    private int _interactableIndex;

    [field: SerializeField] public UnityEvent<IInteractor> OnInteracted { get; private set; }

    private void Awake()
    {
        GrabbableInteractable.OnInteracted.AddListener(OnInteracted.Invoke);
        DroppableInteractable.OnInteracted.AddListener(OnInteracted.Invoke);

        _interactables[0] = GrabbableInteractable;
        _interactables[1] = DroppableInteractable;
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, System.Func<float, float> getGrabSpeed)
    {
        return GrabbableInteractable.GrabCoroutine(grabberParent, getGrabSpeed);
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, float grabTotalTime)
    {
        return GrabbableInteractable.GrabCoroutine(grabberParent, grabTotalTime);
    }

    public IEnumerator InteractCoroutine(IInteractor interactor)
    {
        return GrabbableInteractable.InteractCoroutine(interactor);
    }

    public void Interact(IInteractor interactor)
    {
        _interactables[_interactableIndex].Interact(interactor);
        _interactableIndex = ++_interactableIndex % _interactables.Length;
    }

    public void Drop()
    {
        DroppableInteractable.Drop();
    }
}