using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyMover
{
    private IBaseMovement _movement;

    public void SetBehavior(IBaseMovement movement)
    {
        _movement = movement;
    }

    public void Move(float deltaTime)
    {
        if (_movement != null)
        {
            _movement.Update(deltaTime);
        }
    }

    public void Reset()
    {
        _movement = null;
    }
}
