using System;
using System.Collections.Generic;
using System.Linq;

public class UnitViewHandler
{
    private RefreshButtonHandler _refreshButtonHandler;
    private ProjectileButtonHolder _buttonHandler;
    private CellHandler _cellHandler;
    private int _buttonCount;
    private int _repeatableUnitCount = 0;

    public UnitViewHandler(CellHandler projectileCellHandler, ProjectileButtonHolder buttonHandler, RefreshButtonHandler refreshButton)
    {
        _cellHandler = projectileCellHandler;
        _buttonHandler = buttonHandler;
        _buttonCount = _buttonHandler.ButtonCount;
        _refreshButtonHandler = refreshButton;

        _refreshButtonHandler.Clicked += InitializeButtons;

        InitializeButtons();

        List<ProjectileButton> buttons = _buttonHandler.GetProjectileButtons();

        foreach (ProjectileButton button in buttons)
        {
            button.Disabled += InitializeButtons;
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
}
