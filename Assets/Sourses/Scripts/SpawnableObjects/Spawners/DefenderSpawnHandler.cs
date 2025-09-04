using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawnHandler
{
    private UnitViewHandler _unitViewHandler;
    private SpawnersHandler _spawnersHandler;
    private DefenderData _currentDefenderData;
    private ElementTypes _currentDefenderElementType;

    public DefenderSpawnHandler(UnitViewHandler unitViewHandler, SpawnersHandler spawnersHandler, DefenderData currentDefenderData)
    {
        _unitViewHandler = unitViewHandler;
        _unitViewHandler.PlatformDetected += InitDefender;                 /////////////////////////////////////
        _currentDefenderData = currentDefenderData;        
        _spawnersHandler = spawnersHandler;
    }

    private void InitDefender(UnitPlatform platform, UnitButton button)
    {
        if(platform.IsEmpty == true)
        {
            _currentDefenderElementType = button.Type;
            Vector3 spawnPosition = platform.transform.position;
            Defender defender = _spawnersHandler.SpawnDefender(_currentDefenderData.DefenderType, _currentDefenderElementType, spawnPosition, button.Count);
            platform.Occupy(defender);
        }
        else if(platform.CurrentDefender.ElementType == button.Type) 
        {
            Defender defender = platform.CurrentDefender;
            defender.ProjectileContainer.Recharge(button.Count);
        }
    }
}
