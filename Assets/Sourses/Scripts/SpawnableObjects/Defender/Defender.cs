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
    public List<ElementTypes> ElementTypes { get; private set; }

    private void Awake()
    {
        ProjectileContainer = new DefenderProjectileContainer();
        ProjectileContainer.ProjectileEnded += Disable;
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void Init(List<ElementTypes> types, List<Color> color, SpawnersHandler spawnerHandler, int projectileCount, DefenderConfig config)
    {
        AttackType = config.AttackTypes;
        ProjectileType = config.ProjectileTypes;
        DefenderType = config.DefenderType;
        ProjectileContainer.Recharge(projectileCount);
        _spawnerHandler = spawnerHandler;
        ElementTypes = types;

        _colorGenerator.Init(color);

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new DefenderAttackState(_stateMachine, _defenceAreaDetector, _spawnerHandler, _attackDelay, ProjectileTypes.StandartMagicianProjectile, ElementTypes, _projectileSpawnPoint.position, _defenderAnimatorController, ProjectileContainer));
        _stateMachine.AddState(new DefenderIdleState(_stateMachine, _defenderAnimatorController, _defenceAreaDetector, ElementTypes));
        _stateMachine.SetState<DefenderAttackState>();
    }

    public void Disable()
    {
        Disabled?.Invoke(this);
    }
}
