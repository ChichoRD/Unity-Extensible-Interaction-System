using UnityEngine;

public interface IColliderAccessor
{
    Vector3 ClosestPoint(Vector3 point);
    Vector3 ClosestPointOnBounds(Vector3 point);

    Bounds Bounds { get; }
    bool IsTrigger { get; set; }
    bool Enabled { get; set; }
    GameObject GameObject { get; }
    IRigidbodyAccessor AttachedRigidbody { get; }
}