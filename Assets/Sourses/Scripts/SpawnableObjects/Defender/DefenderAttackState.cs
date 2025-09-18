using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DefenderAttackState : State
{
    private IRechargeable _projectileContainer;
    private DefenderAnimatorController _animatorController;
    private DefenceAreaDetector _detector;
    private SpawnersHandler _spawnerHandler;
    private float _attackTime;
    private ProjectileTypes _projectileType;
    private List<ElementTypes> _currentElement;
    private Vector3 _spawnPosition;

    private UniTask _currentTask;
    private Enemy _currentTarget;

    public DefenderAttackState(
        StateMachine stateMachine, 
        DefenceAreaDetector detector,
        SpawnersHandler spawnerHandler,
        float attackDelay,
        ProjectileTypes projectileType,
        List<ElementTypes> elementType,
        Vector3 spawnPosition,
        DefenderAnimatorController animatorController,
        IRechargeable projectileContainer) : base(stateMachine)
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

    public override void FixedUpdate()
    {
        if(_currentTarget == null)
        {
            _currentTarget = _detector.GetNearbyEnemy(_currentElement);
        }

        if (_currentTarget != null && _currentTask.Status != UniTaskStatus.Pending && _projectileContainer.Count > 0)            //////////////////////////////
        {
            _currentTask = Attack();
        }
        
        if (_currentTarget == null)
        {
            StateMachine.SetState<DefenderIdleState>();
        }
    }

    private async UniTask Attack()
    {
        float defaultClipLength = _animatorController.GetAnimationLength(DefenderAnimationData.AttackClipName);

        float requiredSpeed = defaultClipLength / _attackTime;

        _animatorController.StartAttackAnimation(requiredSpeed);
        _animatorController.ProjectileSpawnPointEnded += SpawnProjectile;
       // _animatorController.AttackAnimationEnded += DecreaseProjectile;

        await UniTask.Delay(TimeSpan.FromSeconds(_attackTime));
    }

    private void SpawnProjectile()
    {
        Projectile currentProjectile = _spawnerHandler.SpawnProjectile(_projectileType, _currentElement.First(), _spawnPosition);           //////////////////////////////////////// Add multi Projectile?
        currentProjectile.SetTarget(_currentTarget);

        if(_currentTarget.IsLastHealth)
        {
            _detector.Delete(_currentTarget);
        }

        _currentTarget = null;
        _animatorController.ProjectileSpawnPointEnded -= SpawnProjectile;
    }

    private void DecreaseProjectile()
    {
        //_animatorController.AttackAnimationEnded -= DecreaseProjectile;
        _projectileContainer.DecreaseCount();
    }
}
