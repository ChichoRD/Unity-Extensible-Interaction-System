using System.Collections.Generic;
using UnityEngine;

public class GrabberInteractor : MonoBehaviour, IGrabberInteractor
{
    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor => _interactorObject as IInteractor;

    [SerializeField] private Transform[] _grabParents;
    [SerializeField] private float _grabTime = 0.25f;
    [SerializeField] private bool _interchangeOnGrabOverflow;
    private Queue<Transform> _occuppiedGrabParents;
    private bool RoomForGrabbing => _occuppiedGrabParents.Count < _grabParents.Length;
    public float GrabSpeed => 1.0f / _grabTime;

    private void Awake() => _occuppiedGrabParents = new Queue<Transform>(_grabParents.Length);

    public Transform GetGrabParent()
    {
        if (RoomForGrabbing)
        {
            Transform grabParent = _grabParents[_occuppiedGrabParents.Count];
            _occuppiedGrabParents.Enqueue(grabParent);
            return grabParent;
        }

        if (_interchangeOnGrabOverflow)
        {
            Transform grabParent = _occuppiedGrabParents.Dequeue();
            _occuppiedGrabParents.Enqueue(grabParent);
            return grabParent;
        }

        return null;
    }

    public IEnumerable<IInteractable> GetInteractables() => Interactor.GetInteractables();
}