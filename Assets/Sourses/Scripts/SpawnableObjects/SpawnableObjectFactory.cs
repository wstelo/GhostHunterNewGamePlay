using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reflex;
using System.ComponentModel;
using Reflex.Attributes;
using Reflex.Injectors;
using UnityEngine.SceneManagement;
using Reflex.Extensions;

public class SpawnableObjectFactory
{   
    public T GetNewSpawnableObject<T>(T prefab) where T : MonoBehaviour, ISpawnableObject<T>
    {
        T item = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);

        return item;
    }
}
