using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorWindowEnemyCell 
{
    public List<ElementTypes> ElementTypes;
    public EnemyTypes EnemyType { get; private set; }
    public int Count { get; private set; }
    public int Health = 1;
    public bool IsMultipleElements;

    public EditorWindowEnemyCell(List<ElementTypes> elementType, EnemyTypes enemyType, int count, int health = 1, bool isMultiple = false)
    {
        EnemyType = enemyType;
        Count = count;
        ElementTypes = elementType;
        IsMultipleElements = isMultiple;
        Health = health;
    }

    public void SetElements(List<ElementTypes> type)
    {
        ElementTypes = type;
    }

    public void SetParameters(List<ElementTypes> elementType, EnemyTypes enemyType, int count, int health, bool IsMultiple)
    {
        ElementTypes = elementType;
        EnemyType = enemyType;
        Count = count;
        Health = health;
        IsMultipleElements = IsMultiple;
    }
}
