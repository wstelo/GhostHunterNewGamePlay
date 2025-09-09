using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawnHandler
{
    private MixingArea _mixingArea;
    private UnitViewHandler _unitViewHandler;
    private SpawnersHandler _spawnersHandler;
  //  private DefenderData _currentDefenderData;
    private ElementTypes _currentDefenderElementType;
    private List<ElementTypes> _currentDefenderElementTypes = new List<ElementTypes>();

    //public DefenderSpawnHandler(UnitViewHandler unitViewHandler, SpawnersHandler spawnersHandler, DefenderData currentDefenderData, MixingArea mixingArea)
    //{
    //    _unitViewHandler = unitViewHandler;
    // //   _unitViewHandler.PlatformDetected += SpawnDefender;                 /////////////////////////////////////
    //    _currentDefenderData = currentDefenderData;        
    //    _spawnersHandler = spawnersHandler;
    //    _mixingArea = mixingArea;
    //  //  _mixingArea.PlatformDetected += SpawnMultiDefender;
    //}

    //private void SpawnDefender(UnitPlatform platform, UnitButton button)
    //{
    //    if(platform.IsEmpty == true)
    //    {
    //        _currentDefenderElementTypes.Add(button.ElementType);
    //        Vector3 spawnPosition = platform.transform.position;
    //        Defender defender = _spawnersHandler.SpawnDefender(_currentDefenderData.DefenderType, _currentDefenderElementTypes, spawnPosition, button.Count);
    //        platform.Occupy(defender);
    //        _currentDefenderElementTypes.Clear();
    //    }
    //    else if(platform.CurrentDefender.ElementTypes.Contains(button.ElementType)) 
    //    {
    //        Defender defender = platform.CurrentDefender;
    //        defender.ProjectileContainer.Recharge(button.Count);
    //    }
    //}

    //private void SpawnMultiDefender(UnitPlatform platform, MixingArea mixingButton)
    //{
    //    if (platform.IsEmpty == true)
    //    {
    //        _currentDefenderElementTypes = mixingButton.ElementTypes;
    //        Vector3 spawnPosition = platform.transform.position;
    //        Defender defender = _spawnersHandler.SpawnDefender(_currentDefenderData.DefenderType, _currentDefenderElementTypes, spawnPosition, mixingButton.Count);
    //        platform.Occupy(defender);
    //        _currentDefenderElementTypes.Clear();
    //    }
    //}
}
