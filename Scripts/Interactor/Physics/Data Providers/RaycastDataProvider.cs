using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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

    private void OnDrawGizmosSelected()
    {
        if (DirectionProvider == null || RadialCastDataProvider == null) return;

        try
        {
            Vector3 capsuleMidPoint = InteractionPosition + 0.5f * InteractionDistance * InteractionDirection;
            Vector3 capsuleScale = new Vector3(_raycastThickness, InteractionDistance * 0.5f, _raycastThickness);
            Quaternion capsuleRotation = Quaternion.LookRotation(Vector3.Cross(InteractionDirection, Random.onUnitSphere), InteractionDirection);

            Gizmos.color = Color.yellow;
            var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Mesh capsuleMesh = capsule.GetComponent<MeshFilter>().sharedMesh;

            Gizmos.DrawWireMesh(capsuleMesh, capsuleMidPoint, capsuleRotation, capsuleScale);
            //Gizmos.DrawRay(InteractionPosition, InteractionDirection * InteractionDistance);
            DestroyImmediate(capsule);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            throw;
        }
    }
}
