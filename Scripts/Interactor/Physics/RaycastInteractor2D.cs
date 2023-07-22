using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class RaycastInteractor2D : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(IRaycastDataProvider<RaycastHit2D>))]
    [SerializeField] private Object _raycastDataProviderObject;
    private IRaycastDataProvider<RaycastHit2D> RaycastDataProvider => _raycastDataProviderObject as IRaycastDataProvider<RaycastHit2D>;

    public IEnumerable<IInteractable> GetInteractables()
    {
        return RaycastDataProvider.GetRaycastFunction()()
            .Where(hit => hit.collider != null)
            .SelectMany(hit => hit.collider.gameObject.GetComponents<IInteractable>())
            .Where(interactable => interactable != null);
    }
}
