using System;
using UnityEngine;

public class DefenderBuilder
{
    private BuildPreviewer _buildPreviewer;
    private InputHandler _inputHandler;

    public event Action<UnitPlatform> BuildInstalled;

    public DefenderBuilder(InputHandler inputHandler, BuildPreviewer buildPreviewer)
    {
        _inputHandler = inputHandler;
        _buildPreviewer = buildPreviewer;
    }

    public void TryCreateNewObject(DefenderData data, Color color)
    {        
        _buildPreviewer.Activate(data.BuildPreview, color);
        _inputHandler.CancelButtonReleased += TryInstallNewObject;
    }
    
    private void TryInstallNewObject()
    {
        if (_buildPreviewer.CurrentPreviewBuilding.IsOnPlatform)
        {
            UnitPlatform currentPlatform = _buildPreviewer.CurrentPreviewBuilding.GetCurrentPlatform();

            if(currentPlatform.IsEmpty)
            {
                currentPlatform.Occupy();
                BuildInstalled?.Invoke(currentPlatform);
            }
        }

        _buildPreviewer.DisableBuildPreviewer();
        _inputHandler.CancelButtonReleased -= TryInstallNewObject;
    }
}
