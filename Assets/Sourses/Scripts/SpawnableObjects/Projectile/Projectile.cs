using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ISpawnableObject<Projectile>
{
    [SerializeField] private ProjectileMover _mover;
    [SerializeField] private List<ParticleSystem> particleSystems;

    public ElementTypes Type { get; private set; }

    public event Action<Projectile> Disabled;

    private void OnEnable()
    {
        _mover.TargetAchieved += Disable;
    }

    private void OnDisable()
    {
        _mover.TargetAchieved -= Disable;
    }

    public void Init(ElementTypes type, Color color)
    {
        Type = type;

        foreach (var particle in particleSystems)
        {
            var main = particle.main;
            main.startColor = color;
        }
    }

    public void SetTarget(Enemy target)
    {
        _mover.Init(target);
    }

    public void Disable()
    {
        Disabled?.Invoke(this);
    }
}
