using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorWindowEnemyCell 
{
    public EnemyTypes EnemyType { get; private set; }
    public int Count { get; private set; }
    public bool IsMultipleElements;
    public List<ElementTypes> ElementTypes;
    public int MultipleTypeHealth = 1;

    public EditorWindowEnemyCell(List<ElementTypes> elementType, EnemyTypes enemyType, int count)
    {
        EnemyType = enemyType;
        Count = count;
        ElementTypes = elementType;
        IsMultipleElements = false;
    }

    public void SetElements(List<ElementTypes> type)
    {
        ElementTypes = type;
    }

    public void SetParameters(List<ElementTypes> elementType, EnemyTypes enemyType, int count)
    {
        ElementTypes = elementType;
        EnemyType = enemyType;
        Count = count;
    }
}
