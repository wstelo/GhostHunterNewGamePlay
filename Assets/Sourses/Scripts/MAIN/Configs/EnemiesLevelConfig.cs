using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLevelConfig
{
    public ElementTypes ElementType { get; private set; }
    public EnemyTypes EnemyType { get; private set; }
    public int Count { get; private set; }

    public EnemiesLevelConfig(ElementTypes elementType, EnemyTypes enemyType, int count)
    {
        ElementType = elementType;
        EnemyType = enemyType;
        Count = count;
    }
}
