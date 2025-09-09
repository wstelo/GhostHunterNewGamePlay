using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Reflex.Attributes;
using System.Linq;
using JetBrains.Annotations;

public class UnitPlatform : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private CellDataHolder _cellDataHolder;

    private int _defaultValue = 0;

    [Inject] private SpawnersHandler _spawnersHandler;

    public Defender CurrentDefender {  get; private set; }
    public bool IsEmpty { get; private set; } = true;

    private void Awake()
    {
        _countText.text = _defaultValue.ToString();
        _cellDataHolder.CellsChanged += Occupy;
    }

    private void Occupy(List<ProjectileCell> cells)
    {
        IsEmpty = false;
        CurrentDefender = SpawnDefender(cells);
        CurrentDefender.Disabled += Clear;
        RefreshCountPanel(CurrentDefender.ProjectileContainer.Count);
        CurrentDefender.ProjectileContainer.CountChanged += RefreshCountPanel;
    }

    private void Clear(Defender currentDefender)
    {
        IsEmpty = true;
        RefreshCountPanel(_defaultValue);
        CurrentDefender.ProjectileContainer.CountChanged -= RefreshCountPanel;
        CurrentDefender.Disabled -= Clear;
        CurrentDefender = null;
        _cellDataHolder.Clear();
    }

    private Defender SpawnDefender(List<ProjectileCell> cells)
    {
        List<ElementTypes> cellTypes = new List<ElementTypes>();
        int count = 0;

        foreach (var cell in cells)
        {
            cellTypes.Add(cell.ElementType);

            if (cell.Count > count)
            {
                count = cell.Count;
            }
        }

         return _spawnersHandler.SpawnDefender(DefenderTypes.Magician, cellTypes, transform.position, count);
    }

    private void RefreshCountPanel(int count)
    {
        _countText.text = count.ToString();
    }
}
