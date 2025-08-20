using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class InterfaceSpawner <T> where T : MonoBehaviour, ISpawnableObject<T>
{
    private InterfaceFactory _factory;
    private T _prefab;
    private ObjectPool<T> _pool;
    private int _poolCapacity = 15;
    private int _poolMaxSize = 10;
    private InterfaceObjectData<T> _data;

    public InterfaceSpawner(InterfaceFactory factory, InterfaceObjectData<T> data)
    {
        _factory = factory;
        _data = data;
        _prefab = data.Prefab;
        CreatePool();
    }

    public T EnableObject(Vector3 position)
    {
        T currentObject = _pool.Get();
        currentObject.transform.position = position;
        currentObject.Init(_data.Color, _data.Type);

        return currentObject;
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<T>(
           createFunc: () => CreateObject(),
            actionOnGet: (item) => Initialize(item),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            defaultCapacity: _poolCapacity,
            actionOnDestroy: (item) => DestroyObject(item),
            maxSize: _poolMaxSize);
    }

    private T CreateObject()
    {
        T item = _factory.GetNewSpawnableObject(_prefab);

        return item;
    }

    private void Initialize(T item)
    {
        item.transform.rotation = UnityEngine.Quaternion.identity;
        item.gameObject.SetActive(true);
        item.Disabled += ReleasedObject;
    }

    private void ReleasedObject(T item)
    {
        item.Disabled -= ReleasedObject;
        _pool.Release(item);
    }

    private void DestroyObject(T item)
    {

    }
}
