using System;
using UnityEngine;

public class RadialCastDataProvider3D : RadialCastDataProvider<Collider>
{
    public override Func<Collider[]> GetRadialCastFunction()
    {
        Vector3 sphereCentre = InteractionPosition;
        float sphereRadius = InteractionDistance;
        LayerMask interactionMask = InteractionMask;
        bool useNonAllocMemory = UseNonAllocMemory;

        return useNonAllocMemory ?
            () =>
            {
                Physics.OverlapSphereNonAlloc(sphereCentre, sphereRadius, PhysicsObjectsMemory, interactionMask);
                return PhysicsObjectsMemory;
            } :
            () => Physics.OverlapSphere(sphereCentre, sphereRadius, interactionMask);
    }
}