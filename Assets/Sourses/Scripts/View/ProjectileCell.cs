using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCell
{
    public ProjectileCell(ElementTypes type, int count, Color color)
    {
        Type = type;
        Count = count;
        Color = color;
    }

    public ElementTypes Type { get; private set; }
    public int Count { get; private set; }
    public Color Color { get; private set; }
}
