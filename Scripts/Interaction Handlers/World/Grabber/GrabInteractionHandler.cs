using System.Collections.Generic;
using UnityEngine;

public class GrabInteractionHandler : MonoBehaviour, IGrabInteractionHandler
{
    [SerializeField] private Transform[] _grabParents;
    [SerializeField] [Min(0.0f)] private float _grabTime = 0.25f;
    [SerializeField] private bool _grabInConstantTime;
    private readonly Dictionary<IInteractable, Transform> _interactableToGrabParentMap = new Dictionary<IInteractable, Transform>();

    public float GrabSpeed => 1.0f / _grabTime;
    public bool GrabInConstantTime => _grabInConstantTime;

    public bool TryFreeGrabParent(IInteractable interactable, out Transform grabParent) => _interactableToGrabParentMap.Remove(interactable, out grabParent);
    public bool TryGetGrabParent(IInteractable interactable, out Transform grabParent) => _interactableToGrabParentMap.TryGetValue(interactable, out grabParent);
    public bool TryAssignGrabParent(IInteractable interactable)
    {
        if (_interactableToGrabParentMap.Count >= _grabParents.Length) return false;

        Transform grabParent = _grabParents[_interactableToGrabParentMap.Count];
        _interactableToGrabParentMap.Add(interactable, grabParent);
        return true;
    }
}
