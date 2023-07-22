using System;
using UnityEngine;

public class RaycastDataProvider2D : RaycastDataProvider<RaycastHit2D>
{
    public override Func<RaycastHit2D[]> GetRaycastFunction()
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
                Array.Clear(PhysicsObjectsMemory, 0, PhysicsObjectsMemory.Length);
                Physics2D.CircleCastNonAlloc(raycastOrigin, raycastThickness, raycastDirection, PhysicsObjectsMemory, raycastDistance, interactionMask);
                return PhysicsObjectsMemory;
            },
            (useCircleCast: true, useNonAllocCast: false) => () => Physics2D.CircleCastAll(raycastOrigin, raycastThickness, raycastDirection, raycastDistance, interactionMask),
            (useCircleCast: false, useNonAllocCast: true) => () =>
            {
                Array.Clear(PhysicsObjectsMemory, 0, PhysicsObjectsMemory.Length);
                Physics2D.RaycastNonAlloc(raycastOrigin, raycastDirection, PhysicsObjectsMemory, raycastDistance, interactionMask);
                return PhysicsObjectsMemory;
            },
            (useCircleCast: false, useNonAllocCast: false) => () => Physics2D.RaycastAll(raycastOrigin, raycastDirection, raycastDistance, interactionMask),
        };
    }
}
