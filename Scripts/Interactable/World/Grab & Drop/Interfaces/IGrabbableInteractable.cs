using System;
using System.Collections;
using UnityEngine;

public interface IGrabbableInteractable : ICoroutineInteractable, IConstrainedInteractable
{
    Transform Transform { get; }
    IEnumerator GrabCoroutine(Transform grabberParent, Func<float, float> getGrabSpeed);
    IEnumerator GrabCoroutine(Transform grabberParent, float grabTotalTime);
}
