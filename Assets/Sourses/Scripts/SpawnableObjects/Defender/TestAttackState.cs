using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;

public class TestAttackState : State
{
    private IRechargable _projectileContainer;
    private DefenderAnimatorController _animatorController;
    private DefenceAreaDetector _detector;
    private SpawnersHandler _spawnerHandler;
    private float _attackTime;
    private ProjectileTypes _projectileType;
    private List<ElementTypes> _currentElement;
    private Vector3 _spawnPosition;

    private IDamageable _currentTarget;

    public TestAttackState(
        StateMachine stateMachine,
        DefenceAreaDetector detector,
        SpawnersHandler spawnerHandler,
        float attackDelay,
        ProjectileTypes projectileType,
        List<ElementTypes> elementType,
        Vector3 spawnPosition,
        DefenderAnimatorController animatorController,
        IRechargable projectileContainer) : base(stateMachine)
    {
        _detector = detector;
        _spawnerHandler = spawnerHandler;
        _attackTime = attackDelay;
        _projectileType = projectileType;
        _currentElement = elementType;
        _spawnPosition = spawnPosition;
        _animatorController = animatorController;
        _projectileContainer = projectileContainer;
    }

    public override void Enter()
    {
        if(_currentTarget == null)
        {
            _currentTarget = _detector.GetNearbyEnemy(_currentElement);
        }

        if(_currentTarget != null )
        {
            Attack();
        }
    }

    private void Attack()
    {
        float defaultClipLength = _animatorController.GetAnimationLength(DefenderAnimationData.AttackClipName);

        float requiredSpeed = defaultClipLength / _attackTime;

        _animatorController.StartAttackAnimation(requiredSpeed);
        _animatorController.ProjectileSpawnPointEnded += SpawnProjectile;
    }

    private void SpawnProjectile()
    {
        _animatorController.ProjectileSpawnPointEnded -= SpawnProjectile;
        Projectile currentProjectile = _spawnerHandler.SpawnProjectile(_projectileType, _currentElement.First(), _spawnPosition);           //////////////////////////////////////// Add multi Projectile?
        
        currentProjectile.SetTarget(_currentTarget);

        Debug.Log(_currentTarget);

        if (_currentTarget.IsLastHealth)
        {
            _detector.Delete(_currentTarget);
        }

        _projectileContainer.DecreaseCount();
        _currentTarget = null;

        ChangeStateToIdleWithDelay().Forget();
    }

    private async UniTask ChangeStateToIdleWithDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_attackTime));

        StateMachine.SetState<DefenderIdleState>();
    }
}
