using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class RaycastDataProvider<T> : MonoBehaviour, IRaycastDataProvider<T>
{
    [SerializeField][Min(0.0f)] private float _raycastThickness = 0.0f;
    public float InteractionThickness => _raycastThickness;

    [RequireInterface(typeof(IPhysicsCastDirectionProvider))]
    [SerializeField] private Object _directionProviderObject;
    private IPhysicsCastDirectionProvider DirectionProvider => _directionProviderObject as IPhysicsCastDirectionProvider;
    public Vector3 InteractionDirection => DirectionProvider.InteractionDirection;

    [RequireInterface(typeof(IRadialCastDataProvider))]
    [SerializeField] private Object _radialCastDataProviderObject;
    private IRadialCastDataProvider RadialCastDataProvider => _radialCastDataProviderObject as IRadialCastDataProvider;
    public Vector3 InteractionPosition => RadialCastDataProvider.InteractionPosition;
    public float InteractionDistance => RadialCastDataProvider.InteractionDistance;
    public LayerMask InteractionMask => RadialCastDataProvider.InteractionMask;

    [SerializeField] private PhysicsCastAllocationProvider<T> _allocationProvider;
    public bool UseNonAllocMemory => ((IPhysicsCastAllocationProvider<T>)_allocationProvider).UseNonAllocMemory;
    public T[] PhysicsObjectsMemory => ((IPhysicsCastAllocationProvider<T>)_allocationProvider).PhysicsObjectsMemory;

    public abstract Func<T[]> GetRaycastFunction(); 
}