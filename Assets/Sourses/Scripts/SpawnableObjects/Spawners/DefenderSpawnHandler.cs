using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawnHandler
{
    private UnitViewHandler _unitViewHandler;
    private DefenderBuilder _builder;
    private SpawnersHandler _spawnersHandler;
    private DefenderData _currentDefenderData;
    private ElementTypes _currentDefenderElementType;
    private ProjectileButton _currentProjectileButton;

    public DefenderSpawnHandler(UnitViewHandler unitViewHandler, DefenderBuilder builder,SpawnersHandler spawnersHandler, DefenderData currentDefenderData)
    {
        _unitViewHandler = unitViewHandler;
        _unitViewHandler.PlatformDetected += InitDefender;                 /////////////////////////////////////
        _builder = builder;
        _currentDefenderData = currentDefenderData;        
        _spawnersHandler = spawnersHandler;
      //  _builder.BuildInstalled += InitDefender;
    }

    private void SetPreview(ProjectileButton button)
    {
        _currentProjectileButton = button;
        _currentDefenderElementType = button.Type;
        _builder.TryCreateNewObject(_currentDefenderData, button.Color);
    }

    private void InitDefender(UnitPlatform platform, ProjectileButton button)
    {
        _currentProjectileButton = button;
        _currentDefenderElementType = button.Type;
        Vector3 spawnPosition = platform.transform.position;
        Defender defender = _spawnersHandler.SpawnDefender(_currentDefenderData.DefenderType, _currentDefenderElementType, spawnPosition, _currentProjectileButton.Count);
        platform.Occupy(defender);
        _currentProjectileButton = null;
    }
}
