using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestAttackState : State
{
    private IRechargeable _projectileContainer;
    private DefenderAnimatorController _animatorController;
    private DefenceAreaDetector _detector;
    private SpawnersHandler _spawnerHandler;
    private float _attackTime;
    private ProjectileTypes _projectileType;
    private List<ElementTypes> _currentElement;
    private Vector3 _spawnPosition;

    private Enemy _currentTarget;

    private CancellationTokenSource _source;

    private bool _isPerformedAttack = false;


    private Defender _denfender;

    public TestAttackState(
        StateMachine stateMachine,
        DefenceAreaDetector detector,
        SpawnersHandler spawnerHandler,
        float attackDelay,
        ProjectileTypes projectileType,
        List<ElementTypes> elementType,
        Vector3 spawnPosition,
        DefenderAnimatorController animatorController,
        IRechargeable projectileContainer,
        Defender denfender) : base(stateMachine)
    {
        _detector = detector;
        _spawnerHandler = spawnerHandler;
        _attackTime = attackDelay;
        _projectileType = projectileType;
        _currentElement = elementType;
        _spawnPosition = spawnPosition;
        _animatorController = animatorController;
        _projectileContainer = projectileContainer;
        _denfender = denfender;
    }

    public override void Enter()
    {
        if (_currentTarget == null)
        {
            _currentTarget = _detector.GetNearbyEnemy(_currentElement);
        }

        if (_currentTarget != null && _currentTarget.IsMarked == false)
        {
            _source?.Cancel();
            _source = new CancellationTokenSource();
            Attack(_source.Token).Forget();
        }
        else
        {
            StateMachine.SetState<DefenderIdleState>();
        }
    }

    public override void Exit()
    {
        if (_isPerformedAttack == false)
        {
            _currentTarget?.RemoveMarked();
        }

        _currentTarget = null;
        _animatorController.StopAttackAnimation();
        _source?.Cancel();

        _isPerformedAttack = false;

        _denfender.SetImage(Color.green);
    }

    private async UniTaskVoid Attack(CancellationToken token)
    {
        if (_currentTarget.IsLastHealth)
        {
            if (_currentTarget.TryMarked(this))
            {
                _denfender.SetImage(Color.red);
            }
            else
            {
                StateMachine.SetState<DefenderIdleState>();
                return;
            }             
        }

        float defaultClipLength = _animatorController.GetAnimationLength(DefenderAnimationData.AttackClipName);

        float requiredSpeed = defaultClipLength / _attackTime;

        _animatorController.StartAttackAnimation(requiredSpeed);

        await UniTask.Delay(TimeSpan.FromSeconds(_attackTime * 0.7), cancellationToken: token);                                      ////////////////////////////////                    Создать класс с определением attackTime множителя

        if (token.IsCancellationRequested)
        {
            return;
        }
        
        if (_currentTarget.CurrentState != this && _currentTarget.IsMarked == true)
        {
            StateMachine.SetState<DefenderIdleState>();
            return;
        }

        Projectile currentProjectile = _spawnerHandler.SpawnProjectile(_projectileType, _currentElement.First(), _spawnPosition);           //////////////////////////////////////// Add multi Projectile?
        currentProjectile.SetTarget(_currentTarget);

        _isPerformedAttack = true;

        await UniTask.Delay(TimeSpan.FromSeconds(_attackTime * 0.3), cancellationToken: token);

        if (token.IsCancellationRequested)
        {
            return;
        }

        _projectileContainer.DecreaseCount();
        StateMachine.SetState<DefenderIdleState>();
    }
}
