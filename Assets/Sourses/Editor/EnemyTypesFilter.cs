using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnemyTypesFilter 
{
    public static (EnemyTypes[] normal, EnemyTypes[] boss) GetEnemyCategories()
    {
        var allTypes = (EnemyTypes[])Enum.GetValues(typeof(EnemyTypes));
        var normal = allTypes.Where(t => !t.ToString().EndsWith("_Boss")).ToArray();
        var boss = allTypes.Where(t => t.ToString().EndsWith("_Boss")).ToArray();
        return (normal, boss);
    }
}
