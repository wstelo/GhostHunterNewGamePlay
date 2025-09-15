using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiProjectileCell
{
    private Action OnConsume;

    public MultiProjectileCell(List<ElementTypes> types, int count, List<Color> colors, Action consumeAction)
    {
        ElementTypes = types;
        Count = count;
        Colors = colors;
        OnConsume = consumeAction;
    }

    public MultiProjectileCell(ElementTypes type, int count, Color color, Action consumeAction)
    { 
        ElementTypes = new List<ElementTypes> { type};
        Count = count;
        Colors = new List<Color> { color }; 
        OnConsume = consumeAction;
    }

    public List<ElementTypes> ElementTypes { get; private set; }
    public int Count { get; private set; }
    public List<Color> Colors { get; private set; }

    public void SetCount(int count)
    {
        Count = count;
    }

    public void Consume()
    {
        OnConsume?.Invoke();
    }
}
