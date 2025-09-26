using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementColorizer
{
    private Dictionary<ElementTypes, Color> _elementColors = new Dictionary<ElementTypes, Color>();

    public ElementColorizer(List<ElementConfig> configs)
    {
        foreach (var config in configs)
        {
            if(_elementColors.Count == 0)
            {
                _elementColors.Add(config.Type, config.Color);
            }

            if(_elementColors.ContainsKey(config.Type) == false)
            {
                _elementColors.Add(config.Type, config.Color);
            }
        }
    }

    public Color GetColorByElementType(ElementTypes type)
    {
        if(_elementColors.TryGetValue(type, out var color))
        {
            return color;
        }

        return Color.white;
    }
}
