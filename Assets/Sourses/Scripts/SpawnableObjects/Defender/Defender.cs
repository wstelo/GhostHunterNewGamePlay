using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DefenderAreaDetector))]
public abstract class Defender : MonoBehaviour, ISpawnableObject<Defender>
{
    [SerializeField] private DefenderAnimatorController _defenderAnimatorController;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private DefenderAreaDetector _defenceAreaDetector;
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;

    private StateMachine _stateMachine;

    private SpawnersHandler _spawnerHandler;
    private float _attackDelay = 0.5f;

    public event Action<Defender> Disabled;

    public IRechargable ProjectileContainer;                                  ///////////////////////////// ???????????????? ןמה טםעונפויסמל לויבט?  ÇÀ×ÅÌ ??????????????????
    public DefenderAttackTypes AttackType { get; private set; }
    public ProjectileTypes ProjectileType { get; private set; }                           ///////////////////////////////////////     DATA HOLDER REQUIRED
    public DefenderTypes DefenderType { get; private set; }
    public ElementTypes ElementType { get; private set; }
    public Color Color { get; private set; } = Color.white;

    private void Awake()
    {
        ProjectileContainer = new DefenderProjectileContainer();
        ProjectileContainer.ProjectileEnded += Disable;
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void Init(ElementTypes type, Color color, SpawnersHandler spawnerHandler, int projectileCount, DefenderData config)
    {
        AttackType = config.AttackTypes;
        ProjectileType = config.ProjectileType;
        DefenderType = config.DefenderType;
        ProjectileContainer.Recharge(projectileCount);
        _spawnerHandler = spawnerHandler;
        ElementType = type;
        Color = color;

        _colorGenerator.Init(new List<Color> { color});

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new DefenderAttackState(_stateMachine, _defenceAreaDetector, _spawnerHandler, _attackDelay, ProjectileTypes.StandartMagicianProjectile, ElementType, _projectileSpawnPoint.position, _defenderAnimatorController, ProjectileContainer));
        _stateMachine.AddState(new DefenderIdleState(_stateMachine, _defenderAnimatorController, _defenceAreaDetector, ElementType));
        _stateMachine.SetState<DefenderAttackState>();
    }

    public void Disable()
    {
        Disabled?.Invoke(this);
    }
}
