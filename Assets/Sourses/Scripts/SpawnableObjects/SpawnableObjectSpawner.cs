using UnityEngine;
using UnityEngine.Pool;

public class SpawnableObjectSpawner<T> where T : MonoBehaviour, ISpawnableObject<T>
{
    private SpawnableObjectFactory _factory;
    private T _prefab;
    private ObjectPool<T> _pool;
    private int _poolCapacity = 15;
    private int _poolMaxSize = 10;

    public SpawnableObjectSpawner(SpawnableObjectFactory factory, T prefab)
    {
        _factory = factory;
        _prefab = prefab;
        CreatePool();
    }

    public T EnableObject(Vector3 position)
    {
        T currentObject = _pool.Get();
        currentObject.transform.position = position;
        currentObject.Disabled += ReleasedObject;

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
