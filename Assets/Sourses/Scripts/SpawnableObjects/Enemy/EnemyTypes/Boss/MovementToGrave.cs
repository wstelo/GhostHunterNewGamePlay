using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MovementToGrave : IBaseMovement
{
    private Transform _currentTarget;

    public MovementToGrave(Transform currentTarget)
    {
        _currentTarget = currentTarget;
    }

    public void Update(float deltaTime)
    {
        
    }
}
