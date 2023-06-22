using System;
using UnityEngine;

public class PhysicsCastAllocationProvider<T> : MonoBehaviour, IPhysicsCastAllocationProvider<T>
{
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

    private T[] _physicsObjectsMemory = null;
    public T[] PhysicsObjectsMemory => _physicsObjectsMemory;

    [SerializeField] private bool _useNonAllocMemory = false;
    public bool UseNonAllocMemory => _useNonAllocMemory;

    private void Awake() => _physicsObjectsMemory = new T[_size];
}
