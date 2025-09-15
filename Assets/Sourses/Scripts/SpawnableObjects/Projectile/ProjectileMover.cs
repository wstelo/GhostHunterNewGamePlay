using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;

    private IDamageable _currentTarget;
    private float _minDistanceToTarget = 1f;
    private Vector3 _offsetY = new Vector3(0,1,0);

    public event Action TargetAchieved;

    private void FixedUpdate()
    {
        if(_currentTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.Transform.position + _offsetY, _moveSpeed * Time.fixedDeltaTime);

            if (transform.position.IsEnoughClose(_currentTarget.Transform.position, _minDistanceToTarget))
            {
                TargetAchieved?.Invoke();
                _currentTarget.TakeDamage();
                _currentTarget = null;
            }
        }
    }

    public void Init(IDamageable target)
    {
        _currentTarget = target;
    }
}
