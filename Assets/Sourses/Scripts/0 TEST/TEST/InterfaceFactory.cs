using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceFactory
{
    public T GetNewSpawnableObject<T>(T prefab) where T : MonoBehaviour, ISpawnableObject<T>
    {
        T item = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);

        return item;
    }
}
