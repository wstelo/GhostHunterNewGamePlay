using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SpawnableObjectData <T> where T : MonoBehaviour, ISpawnableObject<T>
{
    public T Prefab;
    public ElementTypes Type { get; private set; }
    public Color Color { get; private set; }

    protected void Inittialize (ElementTypes type, Color color, T prefab)
    {
        Type = type;
        Color = color;
        Prefab = prefab;
    }
}
