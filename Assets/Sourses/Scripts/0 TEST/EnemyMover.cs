using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyMover : MonoBehaviour
{
    private Vector3 _lastPositionOnSpline = Vector3.zero;
    private Spline _spline;
    private float _minDistance = 0.01f;
    private float _splineValue = 0f;
    private float _speed = 2f;
    private int _allowedSegmentCountForNearestPoint = 10;
    private float _splineLength;

    public event Action SpawnLineWalked;

    public void Init(SplineContainer splineContainer, float speed)
    {
        _speed = speed;
        _spline = splineContainer.Splines.First();
        _splineLength = _spline.GetLength();
    }

    private void FixedUpdate()
    {
        if (_spline != null)
        {
            //if (GetNearbySplinePointAtPercent() > _minDistance)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, _lastPositionOnSpline, _speed * Time.fixedDeltaTime);
            //}
            //else
            //{
            //    MoveToNextPoint();
            //}

            MoveToNextPoint();
        }

        if(_splineValue >= GameStaticData.PercentOfLineToSpawnNewUit / 100)
        {
            SpawnLineWalked?.Invoke();
        }
    }

    private void MoveToNextPoint()
    {
        float distance = _speed * Time.deltaTime;
        float deltaT = distance / _splineLength;

        _splineValue += deltaT;

        if (_splineValue >= 1f)
        {
            _splineValue = 1f;
        }

        _lastPositionOnSpline = SplineUtility.EvaluatePosition(_spline, _splineValue);
        transform.position = _lastPositionOnSpline;
    }

    private float GetNearbySplinePointAtPercent()
    {
        float distance = SplineUtility.GetNearestPoint(_spline, new float3(transform.position.x, transform.position.y, transform.position.z), out float3 position, out float currentPercent, _allowedSegmentCountForNearestPoint);

        return distance;
    }
}
