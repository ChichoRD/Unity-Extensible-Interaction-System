using UnityEngine;

public interface IPhysicsCastMaskProvider
{
    LayerMask InteractionMask { get; }
}
