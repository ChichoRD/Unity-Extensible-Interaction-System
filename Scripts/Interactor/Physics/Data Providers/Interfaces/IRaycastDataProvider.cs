using System;

public interface IRaycastDataProvider : IPhysicsCastPositionProvider, IPhysicsCastDistanceProvider, IPhysicsCastDirectionProvider, IPhysicsCastThicknessProvider, IPhysicsCastMaskProvider { }

public interface IRaycastDataProvider<T> : IRaycastDataProvider, IPhysicsCastAllocationProvider<T>
{
    Func<T[]> GetRaycastFunction();
}