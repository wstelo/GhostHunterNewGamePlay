using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCell
{
    public ProjectileCell(ElementTypes type, int count, Color color)
    {
        ElementType = type;
        Count = count;
        Color = color;
    }

    public ElementTypes ElementType { get; private set; }
    public int Count { get; private set; }
    public Color Color { get; private set; }
}
