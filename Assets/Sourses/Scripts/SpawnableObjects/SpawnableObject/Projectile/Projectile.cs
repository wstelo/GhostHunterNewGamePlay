using System.Collections.Generic;
using UnityEngine;

public class Projectile : SpawnableObject<Projectile>
{
    [SerializeField] private ProjectileMover _mover;
    [SerializeField] private List<ParticleSystem> particleSystems;
    
    private void OnEnable()
    {
        _mover.TargetAchieved += Collected;
    }

    private void OnDisable()
    {
        _mover.TargetAchieved -= Collected;
    }

    public override void Init(Color color, ElementTypes elementType)
    {
        foreach (var particle in particleSystems)                  
        {
            var main = particle.main;
            main.startColor = color;
        }
    }

    public void SetTarget(Ghost target)
    {
        _mover.Init(target);
    }
}
