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

}