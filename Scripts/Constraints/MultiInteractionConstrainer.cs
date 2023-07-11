using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class MultiInteractionConstrainer : MonoBehaviour, IInteractionConstrainer
{
    [Range(0.0f, 0.0f)] private float _constraintMeetingPercentage;
    [SerializeField] private Object[] _constrainerObjects;
    private IEnumerable<IInteractionConstrainer> Constrainers => _constrainerObjects.Cast<IInteractionConstrainer>();

    public bool CanInteract(IInteractor interactor) => Constrainers.Count(constrainer => constrainer.CanInteract(interactor)) / Constrainers.Count() >= _constraintMeetingPercentage;
}

//TODO - Rethink Constrainers and implement interactable layer system