using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadialInteractor2D : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(IRadialCastDataProvider<Collider2D>))]
    [SerializeField] private Object _radialCastDataProviderObject;
    private IRadialCastDataProvider<Collider2D> RadialCastDataProvider => _radialCastDataProviderObject as IRadialCastDataProvider<Collider2D>;

    public IEnumerable<IInteractable> GetInteractables()
    {
        return RadialCastDataProvider.GetRadialCastFunction()()
            .Select(collider => collider.gameObject.GetComponent<IInteractable>())
            .Where(interactable => interactable != null);
    }
}
