using System;
using UnityEngine;

public interface ITriggerDataProvider : IPhysicsCastMaskProvider { }

public interface ITriggerDataProvider<T> : ITriggerDataProvider, IPhysicsCastTriggerProvider<T>, IPhysicsTriggerAllocationProvider<T> where T : Component
{
    Func<T, bool> GetCollisionCacheAdderFunction();
    Func<T, bool> GetCollisionCacheRemoverFunction();
}