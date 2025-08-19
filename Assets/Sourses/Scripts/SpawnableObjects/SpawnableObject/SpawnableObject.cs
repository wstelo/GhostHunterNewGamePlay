using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class SpawnableObject<T> : MonoBehaviour 
    where T : SpawnableObject<T>
{
    public event Action<T> Destroyed;
    public abstract void Init(Color color, ElementTypes elementType);

    public void Collected()
    {
        Destroyed?.Invoke(this as T);
    }   
}
