using System;
using UnityEngine;

public class RaycastDataProvider3D : RaycastDataProvider<RaycastHit>
{
    public override Func<RaycastHit[]> GetRaycastFunction()
    {
        Vector3 raycastOrigin = InteractionPosition;
        float raycastThickness = InteractionThickness;
        Vector3 raycastDirection = InteractionDirection;
        float raycastDistance = InteractionDistance;
        LayerMask interactionMask = InteractionMask;

        bool useNonAllocCast = UseNonAllocMemory;
        bool useCircleCast = raycastThickness > float.Epsilon;

        return (useCircleCast, useNonAllocCast) switch
        {
            (useCircleCast: true, useNonAllocCast: true) => () =>
            {
                Physics.SphereCastNonAlloc(raycastOrigin, raycastThickness, raycastDirection, PhysicsObjectsMemory, raycastDistance, interactionMask);
                return PhysicsObjectsMemory;
            },
            (useCircleCast: true, useNonAllocCast: false) => () => Physics.SphereCastAll(raycastOrigin, raycastThickness, raycastDirection, raycastDistance, interactionMask),
            (useCircleCast: false, useNonAllocCast: true) => () =>
            {
                Physics.RaycastNonAlloc(raycastOrigin, raycastDirection, PhysicsObjectsMemory, raycastDistance, interactionMask);
                return PhysicsObjectsMemory;
            },
            (useCircleCast: false, useNonAllocCast: false) => () => Physics.RaycastAll(raycastOrigin, raycastDirection, raycastDistance, interactionMask),
        };
    }
}