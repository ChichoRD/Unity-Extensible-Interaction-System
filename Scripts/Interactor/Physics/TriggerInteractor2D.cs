using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerInteractor2D : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(ITriggerDataProvider<Collider2D>))]
    [SerializeField] private Object _triggerDataProviderObject;
    private ITriggerDataProvider<Collider2D> TriggerDataProvider => _triggerDataProviderObject as ITriggerDataProvider<Collider2D>;

    public IEnumerable<IInteractable> GetInteractables()
    {
        return TriggerDataProvider.PhysicsObjectsMemory
            .SelectMany(collider => collider.gameObject.GetComponents<IInteractable>())
            .Where(interactable => interactable != null);
    }

    private void OnTriggerEnter2D(Collider2D collision) => TriggerDataProvider.GetCollisionCacheAdderFunction()(collision);
    private void OnTriggerExit2D(Collider2D collision) => TriggerDataProvider.GetCollisionCacheRemoverFunction()(collision);
}
