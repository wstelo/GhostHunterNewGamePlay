using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyMover
{
    private IBaseMovement _movement;
    private SplineContainer _splineContainer;
    private float _speed;
    private Transform _currentTransform;
    private float _minDistanceToSplinePoint = 0.1f;

    public event Action TargetAchieved;

    public void Initialize(SplineContainer splineContainer, Transform currentTransform, float speed)
    {
        _splineContainer = splineContainer;
        _speed = speed;
        _currentTransform = currentTransform;

        _movement = new SplineMovement(_splineContainer, _speed, _currentTransform);
    }

    public async UniTaskVoid SetNewMovementBehavior(Grave target, CancellationToken token)                          //////////////////// 2 Ðàçà âûçûâàåòñÿ                     ÎÁÐÀÁÎÒÀÒÜ ÎØÈÁÊÓ
    {
        float requiredPercentOnSpline = NearestPointOnSplineCalculatorExtension.GetNearestPointOnPercent(_splineContainer, target.transform);
        Vector3 nearestPositionOnSpline = _splineContainer.Spline.EvaluatePosition(requiredPercentOnSpline);

        await UniTask.WaitUntil(() => _currentTransform.position.IsEnoughClose(nearestPositionOnSpline, _minDistanceToSplinePoint), cancellationToken: token);

        if(token.IsCancellationRequested)
        {
            return;
        }

        _movement = new MovementToGrave(target.transform, _currentTransform, _speed);

        await UniTask.WaitUntil(() => _currentTransform.position.IsEnoughClose(target.transform.position, _minDistanceToSplinePoint), cancellationToken: token);

        if (token.IsCancellationRequested)
        {
            return;
        }

        TargetAchieved?.Invoke();
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
