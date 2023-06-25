using System;
using UnityEngine;

[Serializable]
public class PhysicsCastAllocationProvider<T> : /*MonoBehaviour,*/ IPhysicsCastAllocationProvider<T>
{
    private const int DEFAULT_SIZE = 16;

    [SerializeField] [Min(0)] private int _size = 0;
    public int Size 
    {
        get => _size; 
        set
        {
            _size = value;
            Array.Resize(ref _physicsObjectsMemory, _size);
        }
    }

    private T[] _physicsObjectsMemory = new T[DEFAULT_SIZE];
    public T[] PhysicsObjectsMemory => _physicsObjectsMemory;

    [SerializeField] private bool _useNonAllocMemory = false;
    public bool UseNonAllocMemory => _useNonAllocMemory;
}
