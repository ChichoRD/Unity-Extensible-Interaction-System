using UnityEngine;

public class FirstPersonDataProvider : MonoBehaviour, IPhysicsCastPositionProvider, IPhysicsCastDirectionProvider
{
    [SerializeField] private Transform _cameraTransform;

    public Vector3 InteractionPosition => _cameraTransform == null ? Vector3.zero : _cameraTransform.position;
    public Vector3 InteractionDirection => _cameraTransform == null ? Vector3.forward : _cameraTransform.forward;
}