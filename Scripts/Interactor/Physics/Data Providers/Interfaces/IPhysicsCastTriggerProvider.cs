using UnityEngine;

public interface IPhysicsCastTriggerProvider<T> where T : Component
{
    T CollisionTrigger { get; }
}
