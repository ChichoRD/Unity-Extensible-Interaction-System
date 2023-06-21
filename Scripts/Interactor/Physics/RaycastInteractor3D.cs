using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastInteractor3D : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(IRaycastDataProvider<RaycastHit>))]
    [SerializeField] private Object _raycastDataProviderObject;
    private IRaycastDataProvider<RaycastHit> RaycastDataProvider => _raycastDataProviderObject as IRaycastDataProvider<RaycastHit>;

    public IEnumerable<IInteractable> GetInteractables()
    {
        return RaycastDataProvider.GetRaycastFunction()()
            .Select(hit => hit.collider.gameObject.GetComponent<IInteractable>())
            .Where(interactable => interactable != null);
    }
}