using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, ISpawnableObject<Enemy>
{
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;
    [SerializeField] private EnemyMover _enemyMover;

    public event Action<Enemy> Disabled;

    public bool IsMultiType { get; private set; } = false;
    public EnemyTypes EnemyType { get; private set; }
    public List<ElementTypes> ElementTypes { get; private set; }

    public void Disable()
    {
        _enemyMover.ResetPosition();
        Disabled?.Invoke(this);
    }

    public void Init(List<ElementTypes> type, EnemyTypes enemyType, List<Color> colors)
    {
        ElementTypes = type;
        EnemyType = enemyType;

        IsMultiType = ElementTypes.Count > 1 ? true : false;

        _colorGenerator.Init(colors);
    }

    public void SetMover(SplineContainer spline, float speed)
    {
        _enemyMover.Init(spline, speed);
    }
}
