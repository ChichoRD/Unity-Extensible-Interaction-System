using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PhysicsTriggerAllocationProvider<T> : /*MonoBehaviour,*/ IPhysicsTriggerAllocationProvider<T> where T : Component
{
    [SerializeField] private bool _useNonAllocMemory = false;
    public bool UseNonAllocMemory => _useNonAllocMemory;

    private readonly HashSet<T> _physicsObjectsMemory = new HashSet<T>();
    public HashSet<T> PhysicsObjectsMemory => _physicsObjectsMemory;
}