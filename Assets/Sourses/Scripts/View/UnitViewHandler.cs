using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class UnitViewHandler
{
    private RefreshButtonHandler _refreshButtonHandler;
    private ProjectileButtonHolder _buttonHandler;
    private TestCellHandler _cellHandler;

    public UnitViewHandler(TestCellHandler projectileCellHandler, ProjectileButtonHolder buttonHandler, RefreshButtonHandler refreshButton)
    {
        _cellHandler = projectileCellHandler;
        _buttonHandler = buttonHandler;
        _refreshButtonHandler = refreshButton;                         //////////////////////// Вынести в один класс ? в buttonHandler

        _refreshButtonHandler.Clicked += InitializeButtons;

        List<ProjectileButton> buttons = _buttonHandler.GetProjectileButtons();
    }

    public void InitializeButtons()
    {
        List<ProjectileCell> requiredCells = new List<ProjectileCell>();
        requiredCells = _cellHandler.GetCellsByCurrentEnemies();                               ////////////////////////////////

        if (requiredCells != null && requiredCells.Count <= _buttonHandler.ButtonCount)
        {
            List<ProjectileCell> shuffledCells = requiredCells.OrderBy(x => Guid.NewGuid()).ToList();
            _buttonHandler.SetButtons(shuffledCells);
        }
    }
}
