using System.Collections.Generic;
using UnityEngine;

public class SpawnersHandler<T> where T : MonoBehaviour, ISpawnableObject<T>
{
    private SpawnableObjectFactory _spawnableObjectFactory;
    private Dictionary<ElementTypes, SpawnableObjectSpawner<T>> _spawnableObjectSpawners = new Dictionary<ElementTypes, SpawnableObjectSpawner<T>>();
    private Dictionary<ElementTypes, SpawnableObjectData<T>> _spawnableObjectDatas = new Dictionary<ElementTypes, SpawnableObjectData<T>>();

    public SpawnersHandler(IEnumerable<SpawnableObjectData<T>> spawnableObjectData)
    {
        _spawnableObjectFactory = new SpawnableObjectFactory();

        SetSpawnersData(spawnableObjectData);
    }

    public T Spawn(ElementTypes requiredElement, Vector3 position)
    {
        T spawnableObject;

        if (_spawnableObjectSpawners.TryGetValue(requiredElement, out var spawner))
        {
            var currentSpawner = spawner;
            spawnableObject = currentSpawner.EnableObject(position);

            if (_spawnableObjectDatas.TryGetValue(requiredElement, out var data))
            {
                spawnableObject.Init(data.Type, data.Color);
            }

            return spawnableObject;
        }

        return null;
    }

    private void SetSpawnersData(IEnumerable<SpawnableObjectData<T>> spawnableObjectData)
    {
        foreach (var data in spawnableObjectData)
        {
            _spawnableObjectSpawners.Add(data.Type, new SpawnableObjectSpawner<T>(_spawnableObjectFactory, data));
            _spawnableObjectDatas.Add(data.Type, data);
        }
    }
}
