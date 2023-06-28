using System;
using System.Collections;
using UnityEngine;

public interface IGrabbableInteractable : ICoroutineInteractable
{
    IEnumerator GrabCoroutine(Transform grabberParent, Func<float, float> getGrabSpeed);
    IEnumerator GrabCoroutine(Transform grabberParent, float grabTotalTime);
}
