using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : SpawnableObjectData <Enemy>
{
    public EnemyData(ElementTypes type, Enemy prefab, Color typeColor)
    {
        Inittialize(type, typeColor, prefab);
    }
}
