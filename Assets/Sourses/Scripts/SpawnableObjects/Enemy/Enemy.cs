using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.Timeline;

public abstract class Enemy : MonoBehaviour, ISpawnableObject<Enemy>
{
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;
    [SerializeField] private EnemyGraveDetector _graveDetector;

    protected EnemyHealth _health;
    protected SplineContainer _splineContainer;
    protected float _speed;

    private CancellationTokenSource _source;

    public EnemyMover Mover { get; private set; }

    public event Action<Enemy> Disabled;

    public List<ElementTypes> ElementTypes { get; private set; }              /////////////////////////// ENEMYTYPE править
    public bool IsLastHealth => _health.Count.Value == 1;                          ////////////////////////////////////////////////////////////// Корректное свойство?
    public Transform Transform => transform;                       ///////////////////////////////////// ???????????????????????

    public bool IsMarked = false;

    private void Awake()
    {
        Mover = new EnemyMover();
        Mover.TargetAchieved += Disable;
        _health = new EnemyHealth();
        _health.ValueEnded += Disable;                         ///////////////////////// otpiska? + Refresh HEALTH для ПУЛА
    }

    private void OnEnable()
    {
        IsMarked = false;

        _graveDetector.CurrentGrave
            .Where(grave => grave != null && ElementTypes.ExactMatch(grave.ElementTypes) && grave.IsOccupy == false).
            Subscribe(grave =>
            {
                _source?.Cancel();
                _source = new CancellationTokenSource();
                Mover.SetNewMovementBehavior(grave, _source.Token).Forget();
                grave.Occupy();
            })
            .AddTo(this);                              /////////////////////// Forget?                        ///////////// Disposable 
    }

    public void Marked()
    {
        IsMarked = true;
    }

    public void RemoveMarked()
    {
        IsMarked = false;
    }

    private void OnDisable()
    {
        _source?.Cancel();
    }

    private void Update()
    {
        Mover.Move(Time.deltaTime);
    }

    public void TakeDamage()
    {
        _health.TakeDamage(1);
    }

    public void Disable()
    {
        Mover.Reset();
        Disabled?.Invoke(this);
    }

    public virtual void Init(List<ElementTypes> type, List<Color> colors, int healthCount, SplineContainer splineContainer, float speed)
    {
        ElementTypes = type;
        _health.Init(healthCount);
        _colorGenerator.Init(colors);
        _splineContainer = splineContainer;
        _speed = speed;

        Mover.Initialize(splineContainer, transform, speed);
    }
}
