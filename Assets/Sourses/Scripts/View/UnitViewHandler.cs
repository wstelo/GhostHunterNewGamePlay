using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class UnitViewHandler
{
    private ProjectileButtonHandler _buttonHandler;
    private CellHandler _cellHandler;
    private int _buttonCount;
    private int _repeatableUnitCount = 0;

    public event Action<UnitPlatform, ProjectileButton> PlatformDetected;
    public event Action<MultiProjectileButton, ProjectileButton> MixingAreaDetected;

    public UnitViewHandler(CellHandler projectileCellHandler, ProjectileButtonHandler buttonHandler)
    {
        _cellHandler = projectileCellHandler;
        _buttonHandler = buttonHandler;
        _buttonCount = _buttonHandler.ButtonCount;

        InitializeButtons();
     //   ButtonClickInitialize(_buttonHandler.GetProjectileButtons());
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

    //private void ButtonClickInitialize(List<UnitButton> buttons)
    //{
    //    foreach (var button in buttons)
    //    {
    //        button.PlatformDetected += DetectPlatform;
    //        button.MixingAreaDetected += DetectMixingArea;
    //    }
    //}

    //private void DetectMixingArea(MixingArea area, UnitButton button)
    //{
    //    area.Merge(button.ElementType, button.Count, button.Color);
    //}

    //private void DetectPlatform(UnitPlatform platform, UnitButton button)
    //{
    //    PlatformDetected?.Invoke(platform, button);
    //}
}
