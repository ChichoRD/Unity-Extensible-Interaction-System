using System;

public interface IRadialCastDataProvider : IPhysicsCastPositionProvider, IPhysicsCastDistanceProvider, IPhysicsCastMaskProvider { }

public interface IRadialCastDataProvider<T> : IRadialCastDataProvider, IPhysicsCastAllocationProvider<T>
{
    Func<T[]> GetRadialCastFunction();
}
