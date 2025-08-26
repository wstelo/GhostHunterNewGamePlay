using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(DefenderAreaDetector))]
public abstract class Defender : MonoBehaviour, ISpawnableObject<Defender>
{
    [SerializeField] private PlayerAnimatorController _playerAnimatorController;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _projectileSpawnPoint;

    private DefenderAreaDetector _defenceAreaDetector;
    private Enemy _currentTarger;

    public event Action<Defender> Disabled;
    public event Action<Enemy> Attacked;

    public ElementTypes Type { get; protected set; }
    public Color Color { get; protected set; } = Color.white;

    private void Awake()
    {
        _defenceAreaDetector = GetComponent<DefenderAreaDetector>();
    }

    public void Init(ElementTypes type, Color color)
    {
        Type = type;
        Color = color;
        var main = _particleSystem.main;
        main.startColor = Color;
        _renderer.material.color = color;
    }

    public void Disable()
    {
        Disabled?.Invoke(this);
    }
}
