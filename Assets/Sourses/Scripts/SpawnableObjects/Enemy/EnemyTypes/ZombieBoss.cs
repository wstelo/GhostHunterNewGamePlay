using UniRx;
using UnityEngine;

public class ZombieBoss : Enemy
{
    [SerializeField] private HealthCountView _healthCountView;
    [SerializeField] private EnemyGraveDetector _graveDetector;

    private void Start()
    {
        _health.Count.Subscribe(value => _healthCountView.Init(value)).AddTo(this);             ///////////////////////// Disposable
        _graveDetector.CurrentGrave.
            Where(grave => grave != null && ElementTypes.ExactMatch(grave.ElementTypes)).
            Subscribe(grave => _mover.SetBehavior(new MovementToGrave(grave.transform))).
            AddTo(this);                        ///////////// Disposable      
    }
}
