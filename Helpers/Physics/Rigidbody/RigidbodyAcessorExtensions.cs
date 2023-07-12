using UnityEngine;

public static class RigidbodyAcessorExtensions
{
    public static int EnableRigidbodyCollisions(this IRigidbodyAccessor rigidbodyAccessor)
    {
        rigidbodyAccessor.GetAttachedColliders(out IColliderAccessor[] attachedColliders);
        foreach (IColliderAccessor attachedCollider in attachedColliders)
            attachedCollider.Enabled = true;
        return attachedColliders.Length;
    }

    public static int DisableRigidbodyCollisions(this IRigidbodyAccessor rigidbodyAccessor)
    {
        rigidbodyAccessor.GetAttachedColliders(out IColliderAccessor[] attachedColliders);
        foreach (IColliderAccessor attachedCollider in attachedColliders)
            attachedCollider.Enabled = false;
        return attachedColliders.Length;
    }

    // m * dv = F * dt
    // a = F / m
    // F = m * a
    public static void AddAcceleration(this IRigidbodyAccessor rigidbodyAccessor, Vector3 acceleration)
    {
        rigidbodyAccessor.AddForce(acceleration * rigidbodyAccessor.Mass);
    }

    // m * dv = F * dt
    // I = F * dt
    // F = I / dt
    public static void AddImpulse(this IRigidbodyAccessor rigidbodyAccessor, Vector3 impulse)
    {
        rigidbodyAccessor.AddForce(impulse / Time.fixedDeltaTime);
    }

    // m * dv = F * dt
    // dv = F * dt / m
    // F = m * dv / dt
    public static void AddVelocity(this IRigidbodyAccessor rigidbodyAccessor, Vector3 velocity)
    {
        rigidbodyAccessor.AddForce(rigidbodyAccessor.Mass * velocity / Time.fixedDeltaTime);
    }
}