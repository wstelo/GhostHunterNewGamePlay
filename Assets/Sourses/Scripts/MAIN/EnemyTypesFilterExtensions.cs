using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnemyTypesFilterExtensions
{
    public static bool ExactMatch(this List<ElementTypes> parent, List<ElementTypes> target)
    {
        if (parent == null || target == null) return false;
        return parent.Count == target.Count && parent.All(target.Contains);
    }
}
