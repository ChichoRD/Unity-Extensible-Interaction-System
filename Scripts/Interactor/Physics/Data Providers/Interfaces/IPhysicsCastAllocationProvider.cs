public interface IPhysicsCastAllocationProvider<T> : IPhysicsAllocationProvider
{
    T[] PhysicsObjectsMemory { get; }
}
