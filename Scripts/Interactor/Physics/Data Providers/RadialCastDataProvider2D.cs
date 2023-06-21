using System;
using UnityEngine;

public class RadialCastDataProvider2D : RadialCastDataProvider<Collider2D>
{
    public override Func<Collider2D[]> GetRadialCastFunction()
    {
        Vector2 circleCentre = InteractionPosition;
        float circleRadius = InteractionDistance;
        LayerMask interactionMask = InteractionMask;
        bool useNonAllocMemory = UseNonAllocMemory;

        return useNonAllocMemory ?
            () =>
            {
                Physics2D.OverlapCircleNonAlloc(circleCentre, circleRadius, PhysicsObjectsMemory, interactionMask);
                return PhysicsObjectsMemory;
            } :
            () => Physics2D.OverlapCircleAll(circleCentre, circleRadius, interactionMask);
    }
}
