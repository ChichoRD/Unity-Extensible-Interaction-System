using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GrabbableInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _transform;
    [field: SerializeField] public UnityEvent<IInteractor> OnInteracted { get; private set; }

    public void Interact(IInteractor interactor)
    {
        if (interactor is not IGrabberInteractor grabber || grabber.GetGrabParent() == null) return;
        StartCoroutine(GrabCoroutine(grabber.GetGrabParent(), (t) => grabber.GrabSpeed));
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, Func<float, float> getGrabSpeed)
    {
        Vector3 initialPosition = _transform.position;
        Quaternion initalRotation = _transform.rotation;

        float initialDistance = Vector3.Distance(initialPosition, grabberParent.position);
        float initialAngle = Quaternion.Angle(initalRotation, Quaternion.LookRotation(grabberParent.forward));

        const float MAX_ITERATIONS = 256;
        const float THRESHOLD = 0.01f;
        float nT = 1.0f;
        int i = 0;
        while (nT > THRESHOLD && i < MAX_ITERATIONS)
        {
            float instantGrabSpeed = getGrabSpeed(nT);
            float absDx = instantGrabSpeed * Time.deltaTime;

            Vector3 dx = Vector3.MoveTowards(_transform.position, grabberParent.position, absDx);
            Vector3 dRot = Vector3.RotateTowards(_transform.forward, grabberParent.forward, absDx, absDx);

            _transform.SetPositionAndRotation(dx, Quaternion.LookRotation(dRot));
            nT = Mathf.Max(Vector3.Distance(_transform.position, grabberParent.position) / initialDistance,
                           Quaternion.Angle(_transform.rotation, Quaternion.LookRotation(grabberParent.forward)) / initialAngle);
            i++;
            yield return null;
        }
    }
}
