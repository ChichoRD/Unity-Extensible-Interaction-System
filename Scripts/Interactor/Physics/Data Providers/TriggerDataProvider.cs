using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDataProvider<T> : MonoBehaviour, ITriggerDataProvider<T> where T : Component
{
    [SerializeField] private LayerMask _triggerMask;
    public LayerMask InteractionMask => _triggerMask;

    [SerializeField] private T _collisionTrigger;
    public T CollisionTrigger => _collisionTrigger;

    [SerializeField] private PhysicsTriggerAllocationProvider<T> _allocationProvider;
    public bool UseNonAllocMemory => ((IPhysicsTriggerAllocationProvider<T>)_allocationProvider).UseNonAllocMemory;
    public HashSet<T> PhysicsObjectsMemory => ((IPhysicsTriggerAllocationProvider<T>)_allocationProvider).PhysicsObjectsMemory;

    public Func<T, bool> GetCollisionCacheAdderFunction() => (component) => (InteractionMask == (InteractionMask | 1 << component.gameObject.layer)) || PhysicsObjectsMemory.Add(component);
    public Func<T, bool> GetCollisionCacheRemoverFunction() => (component) => (InteractionMask == (InteractionMask | 1 << component.gameObject.layer)) || PhysicsObjectsMemory.Remove(component);
}