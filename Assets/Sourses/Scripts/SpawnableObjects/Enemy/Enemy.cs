using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public abstract class Enemy : MonoBehaviour, ISpawnableObject<Enemy>, IDamageable
{
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;

    protected EnemyHealth _health;
    protected SplineContainer _splineContainer;
    protected float _speed;

    protected EnemyMover _mover {  get; private set; }

    public event Action<Enemy> Disabled;

    public List<ElementTypes> ElementTypes { get; private set; }              /////////////////////////// ENEMYTYPE править
    public bool IsLastHealth => _health.Count.Value == 1;                          ////////////////////////////////////////////////////////////// Корректное свойство?
    public Transform Transform => transform;

    private void Awake()
    {
        _health = new EnemyHealth();
        _health.ValueEnded += Disable;                         ///////////////////////// otpiska? + Refresh HEALTH для ПУЛА

        _mover = new EnemyMover();
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

        _mover.SetBehavior(new SplineMovement(_splineContainer, _speed, transform));
    }
}
