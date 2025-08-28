using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(DefenderAreaDetector))]
public abstract class Defender : MonoBehaviour, ISpawnableObject<Defender>
{
    [SerializeField] private DefenderAnimatorController _defenderAnimatorController;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private DefenderAreaDetector _defenceAreaDetector;

    private Enemy _currentTarget;
    private DefenderAttacker _attackManager;
    private SpawnerHandler _spawnerHandler;
    private float _attackDelay = 1;

    public event Action<Defender> Disabled;
    public event Action<Enemy> Attacked;

    public abstract DefenderAttackTypes AttackType { get; }
    public abstract List<ProjectileTypes> ProjectilesTypes { get; }
    public abstract DefenderTypes DefenderType { get; }
    public int ProjectileCount { get; private set; }
    public ElementTypes ElementType { get; protected set; }
    public Color Color { get; protected set; } = Color.white;

    private void FixedUpdate()
    {
        _currentTarget = _defenceAreaDetector.GetEnemies();       

        if (_attackManager != null && _currentTarget != null && _currentTarget.ElementType == ElementType)
        {
            transform.LookAt(_currentTarget.transform);
            _attackManager.UpdateAttack(this, _currentTarget);
        }      
    }

    public void Init(ElementTypes type, Color color, SpawnerHandler spawnerHandler, int projectileCount)
    {
        ProjectileCount = projectileCount;
        _spawnerHandler = spawnerHandler;
        ElementType = type;
        Color = color;
        var main = _particleSystem.main;
        main.startColor = Color;
        _renderer.material.color = color;

        _attackManager = new DefenderAttacker(_defenceAreaDetector, _spawnerHandler, AttackType, _attackDelay, ProjectilesTypes.First(), ElementType, _projectileSpawnPoint.position, _defenderAnimatorController);
    }

    public void Disable()
    {
        Disabled?.Invoke(this);
    }
}
