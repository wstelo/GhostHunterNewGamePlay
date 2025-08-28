using System;
using UnityEngine;
using UnityEngine.Splines;

public abstract class Enemy : MonoBehaviour, ISpawnableObject<Enemy>
{
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private Renderer _skinnedMeshRenderer;

    public event Action<Enemy> Disabled;

    public EnemyTypes EnemyType { get; private set; }
    public ElementTypes ElementType { get; private set; }

    public void Disable()
    {
        _enemyMover.ResetPosition();
        Disabled?.Invoke(this);
    }

    public void Init(ElementTypes type, EnemyTypes enemyType, Color color)
    {
        _skinnedMeshRenderer.material.color = color;
        ElementType = type;
        EnemyType = enemyType;
    }

    public void SetMover(SplineContainer spline, float speed)
    {
        _enemyMover.Init(spline, speed);
    }
}
