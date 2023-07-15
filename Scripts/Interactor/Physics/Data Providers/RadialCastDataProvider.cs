using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class RadialCastDataProvider<T> : MonoBehaviour, IRadialCastDataProvider<T>
{
    [SerializeField] [Min(0.0f)] private float _interactionDistance = 0.0f;
    public float InteractionDistance => _interactionDistance;

    [SerializeField] private LayerMask _interactionMask = default;
    public LayerMask InteractionMask => _interactionMask;

    [RequireInterface(typeof(IPhysicsCastPositionProvider))]
    [SerializeField] private Object _positionProviderObject;
    private IPhysicsCastPositionProvider PositionProvider => _positionProviderObject as IPhysicsCastPositionProvider;
    public Vector3 InteractionPosition => PositionProvider.InteractionPosition;

    [SerializeField] private PhysicsCastAllocationProvider<T> _allocationProvider = new PhysicsCastAllocationProvider<T>();
    public bool UseNonAllocMemory => ((IPhysicsCastAllocationProvider<T>)_allocationProvider).UseNonAllocMemory;
    public T[] PhysicsObjectsMemory => ((IPhysicsCastAllocationProvider<T>)_allocationProvider).PhysicsObjectsMemory;

    public abstract Func<T[]> GetRadialCastFunction();

    private void OnDrawGizmosSelected()
    {
        if (PositionProvider == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionPosition, InteractionDistance);
    }
}