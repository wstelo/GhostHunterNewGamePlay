using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler <T> where T : SpawnableObject<T>
{
    private SpawnableObjectFactory _spawnableObjectFactory;
    private List<SpawnableObjectData<T>> _spawnableObjectData = new List<SpawnableObjectData<T>>();
    private Dictionary<ElementTypes, SpawnableObjectSpawner<T>> _spawnableObjectSpawners = new Dictionary<ElementTypes, SpawnableObjectSpawner<T>>();

    public SpawnerHandler(List<SpawnableObjectData<T>> spawnableObjectData)
    {
        _spawnableObjectFactory = new SpawnableObjectFactory();
        _spawnableObjectData = spawnableObjectData;

        SetSpawners();
    }

    public T Spawn(ElementTypes requiredElement, Vector3 position)
    {
        T spawnableObject;

        if(_spawnableObjectSpawners.TryGetValue(requiredElement, out var spawner))
        {
            var currentSpawner = spawner;
            spawnableObject = currentSpawner.EnableObject(position);      

            return spawnableObject;
        }

        return null;
    }

    private void SetSpawners()
    {
        foreach (var data in _spawnableObjectData)
        {
            _spawnableObjectSpawners.Add(data.Type, new SpawnableObjectSpawner<T>(_spawnableObjectFactory, data));
        }
    }
}
