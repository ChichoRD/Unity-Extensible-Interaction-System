using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GrabbableInteractable : MonoBehaviour, IGrabbableInteractable
{
    [SerializeField] private Transform _transform;
    [field: SerializeField] public UnityEvent<IInteractor> OnInteracted { get; private set; }

    public void Interact(IInteractor interactor)
    {
        StartCoroutine(InteractCoroutine(interactor));
    }

    public IEnumerator InteractCoroutine(IInteractor interactor)
    {
        if (interactor is not IGrabberInteractor grabber) yield break;

        var grabParent = grabber.GetGrabParent(this);
        if (grabParent == null) yield break;

        yield return StartCoroutine(grabber.GrabInConstantTime ?
                                    GrabCoroutine(grabParent, 1.0f / grabber.GrabSpeed) :
                                    GrabCoroutine(grabParent, (t) => grabber.GrabSpeed));
        OnInteracted?.Invoke(interactor);
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, Func<float, float> getGrabSpeed)
    {
        Vector3 initialPosition = _transform.position;
        Quaternion initalRotation = _transform.rotation;

        float initialDistance = Vector3.Distance(initialPosition, grabberParent.position);
        float initialAngle = Quaternion.Angle(initalRotation, grabberParent.rotation);

        const float MAX_ITERATIONS = 1024;
        for (int i = 0; i < MAX_ITERATIONS; i++)
        {
            const float THRESHOLD = 0.001f;
            float distance = Vector3.Distance(_transform.position, grabberParent.position);
            float angle = Quaternion.Angle(_transform.rotation, grabberParent.rotation);

            float nT = Mathf.Max(distance / (initialDistance + THRESHOLD),
                                 angle / (initialAngle + THRESHOLD));

            if (nT < THRESHOLD) break;

            float instantGrabSpeed = getGrabSpeed(nT);
            float dx = instantGrabSpeed * Time.deltaTime;
            float dTheta = dx * Mathf.Rad2Deg;

            Vector3 newPosition = Vector3.MoveTowards(_transform.position, grabberParent.position, dx);
            Quaternion newRotation = Quaternion.RotateTowards(_transform.rotation, grabberParent.rotation, dTheta);

            _transform.SetPositionAndRotation(newPosition, newRotation);
            yield return null;
        }

        _transform.SetPositionAndRotation(grabberParent.position, grabberParent.rotation);
        _transform.SetParent(grabberParent);
    }

    public IEnumerator GrabCoroutine(Transform grabberParent, float grabTotalTime)
    {
        Vector3 initialPosition = _transform.position;
        Quaternion initalRotation = _transform.rotation;

        for (float t = 0; t < grabTotalTime; t += Time.deltaTime)
        {
            float nT = t / grabTotalTime;

            Vector3 newPosition = Vector3.Lerp(initialPosition, grabberParent.position, nT);
            Quaternion newRotation = Quaternion.Lerp(initalRotation, grabberParent.rotation, nT);

            _transform.SetPositionAndRotation(newPosition, newRotation);
            yield return null;
        }

        _transform.SetPositionAndRotation(grabberParent.position, grabberParent.rotation);
        _transform.SetParent(grabberParent);
    }
}