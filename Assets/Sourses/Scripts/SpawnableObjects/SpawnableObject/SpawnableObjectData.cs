using UnityEngine;

public class SpawnableObjectData<T> where T : SpawnableObject <T>
{
    private ElementTypes _type;
    private T _prefab;
    private Color _typeColor;

    public ElementTypes Type => _type;
    public T Prefab => _prefab;
    public Color Color => _typeColor;

    public SpawnableObjectData(ElementTypes type, T prefab, Color typeColor)
    {
        _type = type;
        _prefab = prefab;
        _typeColor = typeColor;
    }
}
