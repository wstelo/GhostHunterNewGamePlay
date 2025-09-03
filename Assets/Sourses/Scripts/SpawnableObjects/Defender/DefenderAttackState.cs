using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DefenderAttackState : State
{
    private DefenderProjectileContainer _projectileContainer;
    private DefenderAnimatorController _animatorController;
    private DefenderAreaDetector _detector;
    private SpawnersHandler _spawnerHandler;
    private float _attackTime;
    private ProjectileTypes _projectileType;
    private ElementTypes _currentElement;
    private Vector3 _spawnPosition;

    private UniTask _currentTask;
    private Enemy _currentTarget;

    public DefenderAttackState(
        StateMachine stateMachine, 
        DefenderAreaDetector detector,
        SpawnersHandler spawnerHandler,
        float attackDelay,
        ProjectileTypes projectileType,
        ElementTypes elementType,
        Vector3 spawnPosition,
        DefenderAnimatorController animatorController,
        DefenderProjectileContainer projectileContainer) : base(stateMachine)
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

    public override void Enter()                                           /////////////////////////////////////////////////////
    {
        
    }

    public override void FixedUpdate()
    {
        _currentTarget = _detector.GetNearbyEnemyByType(_currentElement);

        if( _currentTarget == null )
        {
            StateMachine.SetState<DefenderIdleState>();
        }

        if (_currentTarget != null && _currentTarget.ElementTypes.First() == _currentElement && _currentTask.Status != UniTaskStatus.Pending && _projectileContainer.ProjectileCount > 0)            //////////////////////////////
        {
            _currentTask = Attack();
        }
    }

    public override void Exit()                                              ///////////////////////////////////////////////////
    {
        
    }

    private async UniTask Attack()
    {
        float defaultClipLength = _animatorController.GetAnimationLength(DefenderAnimationData.AttackClipName);

        float requiredSpeed = defaultClipLength / _attackTime;

        _animatorController.StartAttackAnimation(requiredSpeed);
        _animatorController.ProjectileSpawnPointEnded += SpawnProjectile;
        _animatorController.AttackAnimationEnded += DecreaseProjectile;

        await UniTask.Delay(TimeSpan.FromSeconds(_attackTime));
    }

    private void SpawnProjectile()
    {
        Projectile currentProjectile = _spawnerHandler.SpawnProjectile(_projectileType, _currentElement, _spawnPosition);
        currentProjectile.SetTarget(_currentTarget);
        _detector.Delete(_currentTarget);
        _currentTarget = null;
        _animatorController.ProjectileSpawnPointEnded -= SpawnProjectile;
    }

    private void DecreaseProjectile()
    {
        _animatorController.AttackAnimationEnded -= DecreaseProjectile;
        _projectileContainer.DecreaseCount();
    }
}
