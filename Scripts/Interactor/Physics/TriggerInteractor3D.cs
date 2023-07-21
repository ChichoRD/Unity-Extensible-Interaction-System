using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInteractor3D : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(ITriggerDataProvider<Collider>))]
    [SerializeField] private Object _triggerDataProviderObject;
    private ITriggerDataProvider<Collider> TriggerDataProvider => _triggerDataProviderObject as ITriggerDataProvider<Collider>;

    [field: SerializeField] public UnityEvent OnModifiedCacheOnTriggerEnter { get; set; }
    [field: SerializeField] public UnityEvent OnModifiedCacheOnTriggerExit { get; set; }

    public IEnumerable<IInteractable> GetInteractables()
    {
        return TriggerDataProvider.PhysicsObjectsMemory
            .SelectMany(collider => collider.gameObject.GetComponents<IInteractable>())
            .Where(interactable => interactable != null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (TriggerDataProvider.GetCacheModifierFunctionOnCollisionEnter()(collision))
            OnModifiedCacheOnTriggerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (TriggerDataProvider.GetCacheModifierFunctionOnCollisionExit()(collision))
            OnModifiedCacheOnTriggerExit?.Invoke();
    }
}