using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Unit : MonoBehaviour, ISpawnableObject<Unit>
{
    [SerializeField] private EnemyMover _enemyMover;

    private MeshRenderer _renderer;

    public event Action<Unit> SpawnLineWalked;
    public event Action<Unit> Disabled;

    public ElementTypes Type { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _enemyMover.SpawnLineWalked += WalkedLine;
    }
    public void Init(Color color, ElementTypes elementType)
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
