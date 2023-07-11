using UnityEngine;

public class ColliderAccessor2D : MonoBehaviour, IColliderAccessor
{
    [SerializeField] private Collider2D _collider;

    public Vector3 ClosestPoint(Vector3 point) => _collider.ClosestPoint(point);

    public Vector3 ClosestPointOnBounds(Vector3 point) => _collider.ClosestPoint(point);

    public Bounds Bounds => _collider.bounds;
    public bool IsTrigger { get => _collider.isTrigger; set => _collider.isTrigger = value; }
    public bool Enabled { get => _collider.enabled; set => _collider.enabled = value; }
    public GameObject GameObject => _collider.gameObject;
    public IRigidbodyAccessor AttachedRigidbody => GameObject.GetComponentInParent<IRigidbodyAccessor>();
}