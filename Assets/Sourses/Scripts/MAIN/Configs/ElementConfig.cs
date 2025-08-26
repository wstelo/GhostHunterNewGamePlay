using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewElementConfig", menuName = "NewElementConfig / NewConfig")]
public class ElementConfig : ScriptableObject
{
    [SerializeField] private ElementTypes _type;
    [SerializeField] private Color _typeColor;

    public ElementTypes Type => _type;
    public Color Color => _typeColor;
}
