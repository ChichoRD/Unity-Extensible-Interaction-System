using System;
using UnityEngine;
using UnityEngine.Events;

public class DroppableInteractable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public UnityEvent<IInteractor> OnInteracted { get; private set; }

    public void Interact(IInteractor interactor)
    {
        throw new NotImplementedException();
    }
}