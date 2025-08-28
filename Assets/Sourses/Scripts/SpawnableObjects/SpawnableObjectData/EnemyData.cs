using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyData : SpawnableObjectData <Enemy>
{
    public EnemyTypes EnemyType { get; private set; }

    public EnemyData(EnemyTypes enemyType, Enemy prefab)
    {
        EnemyType = enemyType;
        Inittialize(prefab);
    }
}
