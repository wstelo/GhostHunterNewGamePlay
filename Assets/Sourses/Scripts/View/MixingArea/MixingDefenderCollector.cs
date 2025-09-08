using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixingDefenderCollector
{
    private List<ElementTypes> _elementTypes = new List<ElementTypes>();

    public List<ElementTypes> ElementTypes => _elementTypes.ToList();
    public bool IsEmpty { get; private set; } = true;
    public int Count { get; private set; }

    public void Merge(ElementTypes type, int count)
    {
        if(count >= 0)
        {
            if(IsEmpty)
            {
                _elementTypes.Add(type);
                Count = count;
                IsEmpty = false;
            }
            else
            {
                _elementTypes.Add(type);

                if(count > Count)
                {
                    Count = count;
                }
            }         
        }
    }

    public void Clear()
    {
        _elementTypes.Clear();
        Count = 0;
        IsEmpty = true;
    }
}
