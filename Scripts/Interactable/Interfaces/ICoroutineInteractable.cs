using System.Collections;

public interface ICoroutineInteractable : IInteractable
{
    IEnumerator InteractCoroutine(IInteractor interactor);
}