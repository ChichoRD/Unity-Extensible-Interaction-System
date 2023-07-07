using System.Collections.Generic;
using UnityEngine;

public class GrabInteractionHandler : MonoBehaviour, IGrabInteractionHandler
{
    [SerializeField] private Transform[] _grabParents;
    [SerializeField] [Min(0.0f)] private float _grabTime = 0.25f;
    [SerializeField] private bool _grabInConstantTime;
    private readonly Dictionary<IInteractable, Transform> _grabParentsMap = new Dictionary<IInteractable, Transform>();

    public float GrabSpeed => 1.0f / _grabTime;
    public bool GrabInConstantTime => _grabInConstantTime;

    public bool FreeGrabParent(IInteractable interactable, out Transform grabParent) => _grabParentsMap.Remove(interactable, out grabParent);
    public Transform GetGrabParent(IInteractable interactable) => _grabParentsMap.TryGetValue(interactable, out var grabParent) ? grabParent : null;
    public bool OnInteractionRequest(IInteractable interactable) => HasGrabParentAssigned(interactable) ||
                                                                        (TryAssignGrabParent(interactable) &&
                                                                        interactable.Interact(this));
    private bool HasGrabParentAssigned(IInteractable interactable) => GetGrabParent(interactable) != null;
    private bool TryAssignGrabParent(IInteractable interactable)
    {
        if (_grabParentsMap.Count >= _grabParents.Length) return false;

        Transform grabParent = _grabParents[_grabParentsMap.Count];
        _grabParentsMap.Add(interactable, grabParent);
        return true;
    }
}
