using System.Collections.Generic;
using UnityEngine;

public interface IRigidbodyAccessor
{
    GameObject GameObject { get; }

    float Mass { get; set; }
    Vector3 Position { get; set; }
    Quaternion Rotation { get; set; }
    Vector3 Velocity { get; set; }
    Vector3 AngularVelocity { get; set; }

    float Drag { get; set; }
    float AngularDrag { get; set; }

    bool IsKinematic { get; set; }

    void AddForce(Vector3 force);
    void AddForceAtPosition(Vector3 force, Vector3 position);
    void AddRelativeForce(Vector3 force);
    void AddTorque(Vector3 torque);

    void MovePosition(Vector3 position);
    void MoveRotation(Quaternion rotation);

    int GetAttachedColliders(out IColliderAccessor[] attachedColliders);
    int GetAttachedColliders(out List<IColliderAccessor> attachedColliders);
}
