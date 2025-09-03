using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class EnemiesLevelConfig
{
    public List<ElementTypes> ElementTypes { get; private set; }
    public EnemyTypes EnemyType { get; private set; }
    public int Count { get; private set; }
    public int Health { get; private set; }
    public bool IsMultiple { get; private set; }

    public EnemiesLevelConfig(List<ElementTypes> elementType, EnemyTypes enemyType, int count, bool isMultiple, int health)
    {
        ElementTypes = elementType;
        EnemyType = enemyType;
        Count = count;
        Health = health;
        IsMultiple = isMultiple;
    }
}
