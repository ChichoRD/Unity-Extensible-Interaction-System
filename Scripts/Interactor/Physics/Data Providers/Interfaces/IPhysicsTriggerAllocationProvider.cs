using System.Collections.Generic;
using UnityEngine;

public interface IPhysicsTriggerAllocationProvider<T> : IPhysicsAllocationProvider where T : Component
{
    HashSet<T> PhysicsObjectsMemory { get; }
}
