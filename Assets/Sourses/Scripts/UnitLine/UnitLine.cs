using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitLine : MonoBehaviour
{
    [SerializeField] private PointCollector _pointCollector;
    [SerializeField] private UnitLineMover _unitLineMover;

    private UnitLineMover _lineMover;
    private List<Ghost> _ghosts;

    public event Action<UnitLine> LineEmtied;
    public event Action<UnitLine> SpawnLineWalked;

    public PointCollector PointCollector => _pointCollector;

    private void Awake()
    {
        _lineMover = GetComponent<UnitLineMover>();
    }

    private void OnEnable()
    {
        _lineMover.LineWalked += WalkedSpawnLine;
    }

    private void OnDisable()
    {
        _lineMover.LineWalked -= WalkedSpawnLine;
    }

    public Ghost GetFirstUnit()
    {
        return _ghosts.First();
    }

    public void Init(List<Ghost> ghosts, float moveSpeed)
    {
        _ghosts = ghosts;
        _lineMover.Init(moveSpeed);

        foreach (var ghost in _ghosts)
        {
            ghost.Destroyed += DestroyedUnit;
        }
    }

    public List<ElementTypes> GetElementTypes()
    {
        List<ElementTypes> elementTypes = new List<ElementTypes>();

        foreach (Ghost ghost in _ghosts)
        {
            elementTypes.Add(ghost.Type);
        }

        return elementTypes;
    }

    private void DestroyedUnit(Ghost ghost)
    {
        _ghosts.Remove(ghost);                    ////////////////////////////////////

        if(_ghosts.Count == 0)
        {
            LineEmtied?.Invoke(this);
        }
    }

    private void WalkedSpawnLine()
    {
        SpawnLineWalked?.Invoke(this);
    }

}
