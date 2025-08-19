using System;
using System.Collections.Generic;
using System.Linq;

public class ProjectileViewHandler
{
    private ProjectileButtonHandler _buttonHandler;
    private ChargingPrjectileCellHandler _chargingPrjectileCellHandler;
    private ProjectileCellHandler _cellHandler;
    private int _buttonCount;
    private List<ChargingProjectileCell> _cellList = new List<ChargingProjectileCell>();
    private int _repeatableUnitCount = 0;

    public ProjectileViewHandler(ProjectileCellHandler projectileCellHandler, ProjectileButtonHandler buttonHandler, ChargingPrjectileCellHandler chargingPrjectileCellHandler)
    {
        _cellHandler = projectileCellHandler;
        _buttonHandler = buttonHandler;
        _buttonCount = _buttonHandler.ButtonCount;
        _chargingPrjectileCellHandler = chargingPrjectileCellHandler;

        InitializeButtons();
        ButtonClickInitialize(_buttonHandler.GetProjectileButtons());
        InitializeChargingCell();
    }

    public List<ChargingProjectileCell> GetProjectilesCells()
    {
        return _cellList;
    }

    public void SwapProjectileCells()
    {
        ChargingProjectileCell firstCell = _cellList.First();
        ChargingProjectileCell secondCell = _cellList.Last();

        if (firstCell.IsActive == false && secondCell.IsActive == true)
        {
            firstCell.Init(secondCell.Type, secondCell.Count, secondCell.Color);
            secondCell.Deactivate();
        }
    }

    private void InitializeChargingCell()
    {
        _cellList = _chargingPrjectileCellHandler.GetChargingProjectileCells();

        foreach (var item in _cellList)
        {
            item.Deactivate();
        }
    }

    public void InitializeButtons()
    {
        if (_repeatableUnitCount == 0)
        {
            List<ProjectileCell> requiredCells = new List<ProjectileCell>();
            requiredCells = _cellHandler.GetRequiredProjectileCells();

            if (requiredCells != null)
            {
                int requiredButtonCount = _buttonCount - requiredCells.Count;
                _repeatableUnitCount = GetRepeatableCounts(requiredCells);

                for (int i = 0; i < requiredButtonCount; i++)
                {
                    requiredCells.Add(_cellHandler.GetRandomProjectileCell());
                }

                List<ProjectileCell> shuffledCells = requiredCells.OrderBy(x => Guid.NewGuid()).ToList();
                _buttonHandler.SetButtons(shuffledCells);
            }
        }

        _repeatableUnitCount--;
    }

    private int GetRepeatableCounts(List<ProjectileCell> requiredCells)
    {
        int count = 0;

        foreach (var cell in requiredCells)
        {
            count += cell.Count;
        }

        return count;
    }

    private void ButtonClickInitialize(List<ProjectileButton> buttons)
    {
        foreach (var button in buttons)
        {
            button.ButtonClicked += ChargeProjectile;
            button.ButtonDestroyed += Unsubscribe;
        }
    }

    private void ChargeProjectile(ProjectileButton button)
    {
        foreach (var cell in _cellList)
        {
            if (cell.IsActive == false)
            {
                cell.Init(button.Type, button.Count, button.Color);
                button.Deactivate();

                return;
            }
        }
    }

    private void Unsubscribe(ProjectileButton button)
    {
        button.ButtonClicked -= ChargeProjectile;
        button.ButtonDestroyed -= Unsubscribe;
    }
}
