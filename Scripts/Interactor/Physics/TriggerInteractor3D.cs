using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerInteractor3D : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(ITriggerDataProvider<Collider>))]
    [SerializeField] private Object _triggerDataProviderObject;
    private ITriggerDataProvider<Collider> TriggerDataProvider => _triggerDataProviderObject as ITriggerDataProvider<Collider>;

    public IEnumerable<IInteractable> GetInteractables()
    {
        return TriggerDataProvider.PhysicsObjectsMemory
            .SelectMany(collider => collider.gameObject.GetComponents<IInteractable>())
            .Where(interactable => interactable != null);
    }

    private void OnTriggerEnter(Collider collision) => TriggerDataProvider.GetCollisionCacheAdderFunction()(collision);
    private void OnTriggerExit(Collider collision) => TriggerDataProvider.GetCollisionCacheRemoverFunction()(collision);
}