using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceObjectData <T> where T : MonoBehaviour, ISpawnableObject<T>
{
    private ElementTypes _type;
    private T _prefab;
    private Color _typeColor;

    public ElementTypes Type => _type;
    public T Prefab => _prefab;
    public Color Color => _typeColor;

    public InterfaceObjectData(ElementTypes type, T prefab, Color typeColor)
    {
        _type = type;
        _prefab = prefab;
        _typeColor = typeColor;
    }
}
