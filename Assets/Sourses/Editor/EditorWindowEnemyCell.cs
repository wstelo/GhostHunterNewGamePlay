using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorWindowEnemyCell 
{
    public ElementTypes ElementType { get; private set; }
    public EnemyTypes EnemyType { get; private set; }
    public int Count { get; private set; }

    public EditorWindowEnemyCell(ElementTypes elementType, EnemyTypes enemyType, int count)
    {
        ElementType = elementType;
        EnemyType = enemyType;
        Count = count;
    }

    public void SetElement(ElementTypes type)
    {
        ElementType = type;
    }

    public void SetParameters(ElementTypes elementType, EnemyTypes enemyType, int count)
    {
        ElementType = elementType;
        EnemyType = enemyType;
        Count = count;
    }
}
