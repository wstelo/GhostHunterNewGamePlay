using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineMovement : IBaseMovement
{
    private Transform _transform;
    private float _splineValue = 0f;
    private float _speed = 2f;
    private float _splineLength;
    private Spline _spline;

    public Vector3 PositionOnSpline { get; private set; } = Vector3.zero;

    public SplineMovement(SplineContainer splineContainer, float speed, Transform transform)
    {
        _speed = speed;
        _spline = splineContainer.Splines.First();
        _splineLength = _spline.GetLength();
        _transform  = transform;
    }

    public void Update(float fixedDeltaTime)
    {
        float distance = _speed * Time.deltaTime;
        float deltaT = distance / _splineLength;

        _splineValue += deltaT;

        if (_splineValue >= 1f)
        {
            _splineValue = 1f;
        }

        PositionOnSpline = SplineUtility.EvaluatePosition(_spline, _splineValue);
        _transform.LookAt(PositionOnSpline);
        _transform.position = PositionOnSpline;
    }
}
