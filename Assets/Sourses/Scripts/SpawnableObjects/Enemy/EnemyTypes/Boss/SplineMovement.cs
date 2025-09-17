using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineMovement : IBaseMovement
{
    private Transform _currentTransform;
    private float _splineValue = 0f;
    private float _speed = 2f;
    private float _rotationSpeed = 5f;
    private float _splineLength;
    private Spline _spline;

    private Vector3 _previousPosition;
    private Vector3 _smoothedDirection;

    public Vector3 PositionOnSpline { get; private set; } = Vector3.zero;

    public SplineMovement(SplineContainer splineContainer, float speed, Transform transform)
    {
        _speed = speed;
        _spline = splineContainer.Splines.First();
        _splineLength = _spline.GetLength();
        _currentTransform  = transform;
    }

    public void Update(float deltaTime)
    {
        _previousPosition = _currentTransform.position;

        float distance = _speed * Time.deltaTime;
        float deltaT = distance / _splineLength;

        _splineValue += deltaT;

        if (_splineValue >= 1f)
        {
            _splineValue = 1f;
        }

        PositionOnSpline = SplineUtility.EvaluatePosition(_spline, _splineValue);
        _currentTransform.position = PositionOnSpline;

        Vector3 moveDirection = (_currentTransform.position - _previousPosition).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        _currentTransform.rotation = Quaternion.Slerp(
            _currentTransform.rotation,
            targetRotation,
            _rotationSpeed * deltaTime
        );
    }
}
