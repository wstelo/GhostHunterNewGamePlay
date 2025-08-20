using System;
using UnityEngine;
using UnityEngine.Splines;

public class Ghost : SpawnableObject<Ghost>
{
    [SerializeField] private EnemyMover _enemyMover;

    private MeshRenderer _renderer;

    public event Action<Ghost> SpawnLineWalked;

    public ElementTypes Type { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        _enemyMover.SpawnLineWalked += WalkedLine;
    }

    public override void Init(Color color, ElementTypes elementType)
    {
        _renderer.material.color = color;
        Type = elementType;
    }

    public void InitMover(SplineContainer container, float speed)
    {
        _enemyMover.Init(container, speed);
    }

    private void WalkedLine()
    {
        SpawnLineWalked?.Invoke(this);
    }
}
