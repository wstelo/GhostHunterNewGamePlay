using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerExtensions
{
    public static bool IsContains(this LayerMask thisLayer, LayerMask verifiableLayer)
    {
        return ((1 << verifiableLayer) & thisLayer.value) != 0;
    }
}
