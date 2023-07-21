using UnityEngine;
using UnityEngine.Events;

public class PhysicsLaunchInteractionHandler : MonoBehaviour, IPhysicsLaunchInteractionHandler
{
    [SerializeField] private float _launchImpulseMagnitude = 8.0f;
    [SerializeField] private Transform _relativeTransform;
    public Vector3 LaunchImpulse => _relativeTransform.forward * _launchImpulseMagnitude;
    [field: SerializeField] public UnityEvent<IInteractable> OnAcceptedForInteraction { get; private set; }

}