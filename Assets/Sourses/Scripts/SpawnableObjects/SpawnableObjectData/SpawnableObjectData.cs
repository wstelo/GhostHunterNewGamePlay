using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SpawnableObjectData <T> where T : MonoBehaviour, ISpawnableObject<T>
{
    public T Prefab;

    protected void Inittialize (T prefab)
    {
        Prefab = prefab;
    }
}
