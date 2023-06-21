using System.Collections.Generic;

public interface IInteractor
{
    IEnumerable<IInteractable> GetInteractables();
}
