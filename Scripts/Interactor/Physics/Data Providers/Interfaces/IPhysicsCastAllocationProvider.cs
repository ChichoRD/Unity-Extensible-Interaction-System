public interface IPhysicsCastAllocationProvider<T>
{
    bool UseNonAllocMemory { get; }
    T[] PhysicsObjectsMemory { get; }
}
