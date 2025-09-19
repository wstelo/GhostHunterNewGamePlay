using System.Linq;
using Reflex.Attributes;
using UnityEditor;
using UnityEngine;

public class UnitPlatform : MonoBehaviour
{
    [SerializeField] private ProjectileButtonView _countText;
    [SerializeField] private CellDataHolder _cellDataHolder;

    private MultiProjectileCell _currentCell;

    private DefenderSpawnHandler _spawnersHandler;

    public Defender CurrentDefender { get; private set; }

    private void Awake()
    {
        _countText.ResetCount();
        _cellDataHolder.CellChanged += Occupy;
    }

    public void Initialize(DefenderSpawnHandler spawnerHandler)
    {
        _spawnersHandler = spawnerHandler;
    }

    private void Occupy(MultiProjectileCell cell)
    {
        bool isChanged = false;

        if (CurrentDefender == null)
        {
            isChanged = true;
            _currentCell = cell;
        }
        else
        {
            if (cell.ElementTypes.Count == 1)
            {
                if (_currentCell.ElementTypes.Count > 1)
                {
                    foreach (var type in _currentCell.ElementTypes)
                    {
                        if (type == cell.ElementTypes.First())
                        {
                            isChanged = true;
                            int count = CurrentDefender.ProjectileContainer.Count;
                            _currentCell = cell;
                            _currentCell.SetCount(count);
                            CurrentDefender.Disable();
                        }
                    }
                }
                else
                {
                    if(cell.ElementTypes.First() == _currentCell.ElementTypes.First())
                    {
                        isChanged = true;
                        int count = CurrentDefender.ProjectileContainer.Count;
                        _currentCell = cell;
                        _currentCell.SetCount(_currentCell.Count + count);                                 //////////////////////////////////// multi?
                        CurrentDefender.Disable();
                    }
                }
            }
        }

        if (isChanged)
        {
            CurrentDefender = _spawnersHandler.SpawnDefender(_currentCell, transform.position);
            CurrentDefender.Disabled += Clear;
            CurrentDefender.ProjectileContainer.CountChanged += RefreshCountPanel;
            RefreshCountPanel(CurrentDefender.ProjectileContainer.Count);
            _currentCell.Consume();
        }
    }

    private void Clear(Defender currentDefender)
    {
        _countText.ResetCount();
        CurrentDefender.ProjectileContainer.CountChanged -= RefreshCountPanel;
        CurrentDefender.Disabled -= Clear;
        CurrentDefender = null;
    }

    private void RefreshCountPanel(int count)
    {
        _countText.Init(count, CurrentDefender.Colors.First());                                                    ////////////////////////////////////////////////
    }

    private void OnDrawGizmosSelected()
    {
        Color color = new Color(1,0,1,0.2f);

        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, GameStaticData.UnitPlatformRadius);
    }
}
