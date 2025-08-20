using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSpawnHandler<T> where T : MonoBehaviour, ISpawnableObject<T>
{
    private InterfaceFactory _spawnableObjectFactory;
    private List<InterfaceObjectData<T>> _spawnableObjectData = new List<InterfaceObjectData<T>>();
    private Dictionary<ElementTypes, InterfaceSpawner<T>> _spawnableObjectSpawners = new Dictionary<ElementTypes, InterfaceSpawner<T>>();

    public InterfaceSpawnHandler(List<InterfaceObjectData<T>> spawnableObjectData)
    {
        _spawnableObjectFactory = new InterfaceFactory();
        _spawnableObjectData = spawnableObjectData;

        SetSpawners();
    }

    public T Spawn(ElementTypes requiredElement, Vector3 position)
    {
        T spawnableObject;

        if (_spawnableObjectSpawners.TryGetValue(requiredElement, out var spawner))
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
            _spawnableObjectSpawners.Add(data.Type, new InterfaceSpawner<T>(_spawnableObjectFactory, data));
        }
    }
}
