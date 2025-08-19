using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;

    private Ghost _currentTarget;
    private float _minDistanceToTarget = 1f;
    private Vector3 _offset = new Vector3(0,1,0);

    public event Action TargetAchieved;

    private void FixedUpdate()
    {
        if(_currentTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position + _offset, _moveSpeed * Time.fixedDeltaTime);

            if (transform.position.IsEnoughClose(_currentTarget.transform.position, _minDistanceToTarget))
            {
                TargetAchieved?.Invoke();
                _currentTarget.Collected();
            }
        }
    }

    public void Init(Ghost target)
    {
        _currentTarget = target;
    }
}
