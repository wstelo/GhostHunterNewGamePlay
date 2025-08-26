using System;
using UnityEngine;
using UnityEngine.Splines;

public abstract class Enemy : MonoBehaviour, ISpawnableObject<Enemy>
{
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    public event Action<Enemy> Disabled;

    public ElementTypes Type { get; private set; }
    public void Disable()
    {
        Disabled?.Invoke(this);
    }

    public void Init(ElementTypes type, Color color)
    {
        _skinnedMeshRenderer.material.color = color;
        Type = type;
    }

    public void SetMover(SplineContainer spline, float speed)
    {
        _enemyMover.Init(spline, speed);
    }
}
