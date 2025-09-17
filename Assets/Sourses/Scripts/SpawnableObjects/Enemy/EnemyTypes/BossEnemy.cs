using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Splines;

public class BossEnemy : Enemy
{
    [SerializeField] private HealthCountView _healthCountView;

    private void Start()
    {
        _health.Count.Subscribe(value => _healthCountView.Init(value)).AddTo(this);             ///////////////////////// Disposable
    }
}
