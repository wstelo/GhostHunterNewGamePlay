using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MovementToGrave : IBaseMovement
{
    private Transform _currentTarget;
    private Transform _currentTransform;
    private float _speed;
    private float _rotationSpeed = 5f;

    private Vector3 _lastPosition;
    private Vector3 _direction;

    public MovementToGrave(Transform target, Transform currentTransform, float speed)
    {
        _currentTarget = target;
        _currentTransform = currentTransform;
        _speed = speed;
    }

    public void Update(float deltaTime)
    {
        _lastPosition = _currentTransform.position;

        _currentTransform.position = Vector3.MoveTowards(_currentTransform.position, _currentTarget.position, _speed * deltaTime);

        _direction = _currentTransform.position - _lastPosition;
        
        Quaternion rotation = Quaternion.LookRotation(_direction.normalized);

        _currentTransform.rotation = Quaternion.Slerp(_currentTransform.rotation, rotation, _rotationSpeed * deltaTime);
    }
}
