using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[RequireComponent(typeof(DefenceAreaDetector))]
public abstract class Defender : MonoBehaviour, ISpawnableObject<Defender>
{
    [SerializeField] private DefenderAnimatorController _defenderAnimatorController;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private DefenceAreaDetector _defenceAreaDetector;
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;

    private EnemyCollector _enemyCollector;

    private StateMachine _stateMachine;
    private SpawnersHandler _spawnerHandler;
    private float _attackDelay = 2f;

    public event Action<Defender> Disabled;

    public IRechargeable ProjectileContainer;                                  ///////////////////////////// ???????????????? под интерфейсом мейби?  ЗАЧЕМ ??????????????????
    public ProjectileTypes ProjectileType { get; private set; }                           ///////////////////////////////////////     DATA HOLDER REQUIRED
    public List<ElementTypes> ElementTypes { get; private set; }
    public List<Color> Colors { get; private set; }                         //////////////           nahui?

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

    public void SetEnemyCollector(EnemyCollector enemyCollector)
    {
        _enemyCollector = enemyCollector;
        SetStatesWithDelay().Forget();              //////////////////////////////////////////////////////
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

    }

    private async UniTaskVoid SetStatesWithDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.6f));  /////////////////////////////////////////////////////// Token

        _stateMachine.AddState(new TestAttackState(_stateMachine, _defenceAreaDetector, _spawnerHandler, _attackDelay, ProjectileTypes.StandartMagicianProjectile, ElementTypes, _projectileSpawnPoint.position, _defenderAnimatorController, ProjectileContainer, _enemyCollector));
        _stateMachine.AddState(new DefenderIdleState(_stateMachine, _defenderAnimatorController, _defenceAreaDetector, ElementTypes));          /////////////// Инициализация стейтов?
        _stateMachine.SetState<DefenderIdleState>();
    }

    public void Disable()
    {
        _stateMachine.Reset();
        ProjectileContainer.Clear();
        Disabled?.Invoke(this);
    }
}
