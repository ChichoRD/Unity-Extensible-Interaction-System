using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadialInteractor3D : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(IRadialCastDataProvider<Collider>))]
    [SerializeField] private Object _radialCastDataProviderObject;
    private IRadialCastDataProvider<Collider> RadialCastDataProvider => _radialCastDataProviderObject as IRadialCastDataProvider<Collider>;

    public IEnumerable<IInteractable> GetInteractables()
    {
        return RadialCastDataProvider.GetRadialCastFunction()()
            .SelectMany(collider => collider.gameObject.GetComponents<IInteractable>())
            .Where(interactable => interactable != null);
    }
}