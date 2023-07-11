using UnityEngine;

public interface IDroppableInteractable : IInteractable
{
    Transform Transform { get; }
}