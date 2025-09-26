using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DefenceAreaDetector))]
public abstract class Defender : MonoBehaviour, ISpawnableObject<Defender>
{
    [SerializeField] private DefenderAnimatorController _defenderAnimatorController;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private DefenceAreaDetector _defenceAreaDetector;
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;
    [SerializeField] private AnimationEventReceiver _reciever;

    private StateMachine _stateMachine;
    private SpawnersHandler _spawnerHandler;
    private float _attackDelay = 1f;

    private float _spawnProjectileTriggerTime = 0f;

    public event Action<Defender> Disabled;

    public IRechargeable ProjectileContainer;                                  ///////////////////////////// ???????????????? под интерфейсом мейби?  ЗАЧЕМ ??????????????????
    public ProjectileTypes ProjectileType { get; private set; }                           ///////////////////////////////////////     DATA HOLDER REQUIRED
    public List<ElementTypes> ElementTypes { get; private set; }
    public List<Color> Colors { get; private set; }                         //////////////           nahui?

    private ReactiveProperty<(Enemy, Transform)> _currentTarget = new ReactiveProperty<(Enemy, Transform)>();

    public IReadOnlyReactiveProperty<(Enemy, Transform)> CurrentTarget => _currentTarget;

    private void Awake()
    {
        ProjectileContainer = new DefenderProjectileContainer();
        ProjectileContainer.ProjectileEnded += Disable;                               ////////////////////////////////// OTPISKA

        _stateMachine = new StateMachine();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
    
    public void Init(List<ElementTypes> types, List<Color> color, SpawnersHandler spawnerHandler, int projectileCount, DefenderConfig config)
    {
        ProjectileType = config.ProjectileTypes;
        ProjectileContainer.Recharge(projectileCount);
        _spawnerHandler = spawnerHandler;
        ElementTypes = types;
        Colors = color;
        _colorGenerator.Init(Colors);

        _defenderAnimatorController.StartIdleAnimation();

        SetStatesWithDelay().Forget();
    }

    public void Disable()
    {
        _stateMachine.Reset();
        ProjectileContainer.Clear();
        Disabled?.Invoke(this);
    }

    private async UniTaskVoid SetStatesWithDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.6f));  /////////////////////////////////////////////////////// Token

        _spawnProjectileTriggerTime = _reciever.GetAnimationTriggerTime("IsAttack");

        Debug.Log(_spawnProjectileTriggerTime);

        _stateMachine.AddState(new TestAttackState(_stateMachine, _defenceAreaDetector, _spawnerHandler, _attackDelay, ProjectileTypes.StandartMagicianProjectile, ElementTypes, _projectileSpawnPoint.position, _defenderAnimatorController, ProjectileContainer, _spawnProjectileTriggerTime));
        _stateMachine.AddState(new DefenderIdleState(_stateMachine, _defenderAnimatorController, _defenceAreaDetector, ElementTypes));          /////////////// Инициализация стейтов?
        _stateMachine.SetState<DefenderIdleState>();
    }
}
