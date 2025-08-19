using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransformPoint : IReadOnlyPoint
{
    public Transform Point {  get; private set; }

    public TransformPoint( Transform point)
    {
        Point = point;
    }
}
