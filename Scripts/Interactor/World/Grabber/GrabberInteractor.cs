using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabberInteractor : MonoBehaviour, IGrabberInteractor
{
    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor => _interactorObject as IInteractor;

    [SerializeField] private Transform[] _grabParents;
    [SerializeField] private float _grabTime = 0.25f;
    [SerializeField] private bool _grabInConstantTime;
    [SerializeField] private bool _interchangeOnGrabOverflow;
    private Dictionary<IInteractable, Transform> _occuppiedGrabParents;
    private bool RoomForGrabbing => _occuppiedGrabParents.Count < _grabParents.Length;
    public float GrabSpeed => 1.0f / _grabTime;
    public bool GrabInConstantTime => _grabInConstantTime;

    private void Awake() => _occuppiedGrabParents = new Dictionary<IInteractable, Transform>();

    public Transform GetGrabParent(IInteractable interactable)
    {
        if (RoomForGrabbing)
        {
            Transform grabParent = _grabParents[_occuppiedGrabParents.Count];
            _occuppiedGrabParents.Add(interactable, grabParent);
            return grabParent;
        }

        if (_interchangeOnGrabOverflow)
        {
            IInteractable firstInteractable = _occuppiedGrabParents.Last().Key;
            FreeGrabParent(firstInteractable, out Transform freeGrabParent);
            _occuppiedGrabParents.Add(interactable, freeGrabParent);

            if (firstInteractable is not IDroppableInteractable droppable) return freeGrabParent;

            droppable.Interact(this);
            return freeGrabParent;
        }

        return null;
    }

    public bool FreeGrabParent(IInteractable interactable, out Transform grabParent)
    {
        return _occuppiedGrabParents.Remove(interactable, out grabParent);
    }

    public IEnumerable<IInteractable> GetInteractables() => Interactor.GetInteractables();

}