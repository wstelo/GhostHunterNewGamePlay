using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UnitPlatform : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private CellDataHolder _cellDataHolder;

    private int _defaultValue = 0;

    private SpawnersHandler _spawnersHandler;

    public Defender CurrentDefender {  get; private set; }
    public bool IsEmpty { get; private set; } = true;

    private void Awake()
    {
        _countText.text = _defaultValue.ToString();
        _cellDataHolder.CellsChanged += Init;
    }

    private void Init(List<ProjectileCell> cells)
    {

    }

    public void Occupy(Defender currentDefender)
    {
        IsEmpty = false;
        CurrentDefender = currentDefender;
        CurrentDefender.Disabled += Clear;
        RefreshCountPanel(CurrentDefender.ProjectileContainer.Count);
        CurrentDefender.ProjectileContainer.CountChanged += RefreshCountPanel;
    }

    public void Clear(Defender currentDefender)
    {
        IsEmpty = true;
        RefreshCountPanel(_defaultValue);
        CurrentDefender.ProjectileContainer.CountChanged -= RefreshCountPanel;
        CurrentDefender.Disabled -= Clear;
        CurrentDefender = null;
    }

    private void RefreshCountPanel(int count)
    {
        _countText.text = count.ToString();
    }
}
