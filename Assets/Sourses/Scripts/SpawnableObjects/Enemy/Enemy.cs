using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Splines;

public abstract class Enemy : MonoBehaviour, ISpawnableObject<Enemy>, IDamageable
{
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;
    [SerializeField] private EnemyGraveDetector _graveDetector;

    protected EnemyHealth _health;
    protected SplineContainer _splineContainer;
    protected float _speed;

    protected EnemyMover _mover { get; private set; }

    public event Action<Enemy> Disabled;

    public List<ElementTypes> ElementTypes { get; private set; }              /////////////////////////// ENEMYTYPE править
    public bool IsLastHealth => _health.Count.Value == 1;                          ////////////////////////////////////////////////////////////// Корректное свойство?
    public Transform Transform => transform;                       ///////////////////////////////////// ???????????????????????

    private void Awake()
    {
        _mover = new EnemyMover();
        _mover.TargetAchieved += Disable;
        _health = new EnemyHealth();
        _health.ValueEnded += Disable;                         ///////////////////////// otpiska? + Refresh HEALTH для ПУЛА

        _graveDetector.CurrentGrave.
        Where(grave => grave != null && ElementTypes.ExactMatch(grave.ElementTypes) && grave.IsOccupy == false).
        Subscribe(grave =>
        {
            _mover.SetNewMovementBehavior(grave).Forget();
            grave.Occupy();
        }).AddTo(this);                              /////////////////////// Forget?                        ///////////// Disposable      
    }

    private void Update()
    {
        _mover.Move(Time.deltaTime);
    }

    public void TakeDamage()
    {
        _health.TakeDamage(1);
    }

    public void Disable()
    {
        _mover.Reset();
        Disabled?.Invoke(this);
    }

    public virtual void Init(List<ElementTypes> type, List<Color> colors, int healthCount, SplineContainer splineContainer, float speed)
    {
        ElementTypes = type;
        _health.Init(healthCount);
        _colorGenerator.Init(colors);
        _splineContainer = splineContainer;
        _speed = speed;

        _mover.Initialize(splineContainer, transform, speed);
    }
}
