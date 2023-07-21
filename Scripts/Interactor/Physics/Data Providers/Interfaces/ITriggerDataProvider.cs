using System;
using UnityEngine;

public interface ITriggerDataProvider : IPhysicsCastMaskProvider 
{
    bool CacheOnEnter { get; }
    bool CacheOnExit { get; }
    bool RemoveOnEnter { get; }
    bool RemoveOnExit { get; }
}

public interface ITriggerDataProvider<T> : ITriggerDataProvider, IPhysicsCastTriggerProvider<T>, IPhysicsTriggerAllocationProvider<T> where T : Component
{
    Func<T, bool> GetCacheModifierFunctionOnCollisionEnter();
    Func<T, bool> GetCacheModifierFunctionOnCollisionExit();
}