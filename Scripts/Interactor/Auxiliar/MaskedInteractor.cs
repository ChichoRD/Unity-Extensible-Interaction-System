using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaskedInteractor : MonoBehaviour, IInteractor
{
    [RequireInterface(typeof(IInteractor))]
    [SerializeField] private Object _interactorObject;
    private IInteractor Interactor => _interactorObject as IInteractor;

    [SerializeField] private InteractionLayer _interactionMask = InteractionLayer.InteractionLayer0;
    [SerializeField] private bool _allowPartialMaskPass;

    public IEnumerable<IInteractable> GetInteractables() => Interactor.GetInteractables()
                                                            .Where(interactable => _allowPartialMaskPass
                                                                ? IsPartiallyInMask(interactable, _interactionMask)
                                                                : IsFullyInMask(interactable, _interactionMask));

    public static bool IsPartiallyInMask(IInteractable interactable, InteractionLayer interactionMask) => (interactable.InteractionLayer & interactionMask) != 0;
    public static bool IsFullyInMask(IInteractable interactable, InteractionLayer interactionMask) => (interactable.InteractionLayer & interactionMask) == interactable.InteractionLayer;

}