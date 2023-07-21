using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDataProvider<T> : MonoBehaviour, ITriggerDataProvider<T> where T : Component
{
    [SerializeField] private LayerMask _triggerMask;
    public LayerMask InteractionMask => _triggerMask;

    [SerializeField] private T _collisionTrigger;
    public T CollisionTrigger => _collisionTrigger;

    [SerializeField] private PhysicsTriggerAllocationProvider<T> _allocationProvider = new PhysicsTriggerAllocationProvider<T>();
    public bool UseNonAllocMemory => ((IPhysicsTriggerAllocationProvider<T>)_allocationProvider).UseNonAllocMemory;
    public HashSet<T> PhysicsObjectsMemory => ((IPhysicsTriggerAllocationProvider<T>)_allocationProvider).PhysicsObjectsMemory;

    [SerializeField] private bool _cacheOnEnter = true;
    public bool CacheOnEnter => _cacheOnEnter;

    [SerializeField] private bool _cacheOnExit = false;
    public bool CacheOnExit => _cacheOnExit;

    [SerializeField] private bool _removeOnEnter = false;
    public bool RemoveOnEnter => _removeOnEnter;

    [SerializeField] private bool _removeOnExit = true;
    public bool RemoveOnExit => _removeOnExit;

    public virtual Func<T, bool> GetCacheModifierFunctionOnCollisionEnter()
    {
        return (CacheOnEnter, RemoveOnEnter) switch
        {
            (CacheOnEnter: true, RemoveOnEnter: false) => (component) => InMask(component) && PhysicsObjectsMemory.Add(component),
            (CacheOnEnter: false, RemoveOnEnter: true) => (component) => InMask(component) && PhysicsObjectsMemory.Remove(component),
            _ => (component) => InMask(component)
        };
    }

    public virtual Func<T, bool> GetCacheModifierFunctionOnCollisionExit()
    {
        return (CacheOnExit, RemoveOnExit) switch
        {
            (CacheOnExit: true, RemoveOnExit: false) => (component) => InMask(component) && PhysicsObjectsMemory.Add(component),
            (CacheOnExit: false, RemoveOnExit: true) => (component) => InMask(component) && PhysicsObjectsMemory.Remove(component),
            _ => (component) => InMask(component)
        };
    }

    private bool InMask(T component) => InteractionMask == (InteractionMask | 1 << component.gameObject.layer);
}