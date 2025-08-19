using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnableObjectFactory
{
    public T GetNewSpawnableObject<T>(T prefab) where T : SpawnableObject<T>
    {
        T item = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);

        return item;
    }
}
