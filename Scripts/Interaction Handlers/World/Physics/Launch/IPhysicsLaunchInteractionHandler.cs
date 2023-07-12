using UnityEngine;

public interface IPhysicsLaunchInteractionHandler : IInteractionHandler
{
    Vector3 LaunchImpulse { get; }
}
