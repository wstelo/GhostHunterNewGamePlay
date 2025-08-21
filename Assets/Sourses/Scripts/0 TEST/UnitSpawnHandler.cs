using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnHandler
{
    private UnitViewHandler _unitViewHandler;
    private BuildConstructor _buildConstructor;
    private List<SpawnableObjectData<Ghost>> _unitsData = new List<SpawnableObjectData<Ghost>>();

    public UnitSpawnHandler(UnitViewHandler unitViewHandler, BuildConstructor buildConstructor, List<SpawnableObjectData<Ghost>> unitsData)
    {
        _buildConstructor = buildConstructor;
        _unitViewHandler = unitViewHandler;
        _unitViewHandler.ButtonClicked += SetPreview;
        _unitsData = unitsData;
    }

    private void SetPreview(ProjectileButton button)
    {
        foreach (var item in _unitsData)
        {
            if(item.Type == button.Type)
            {
                _buildConstructor.CreateBuildPreview(item.PreviewPrefab, item.Color);
            }
        }
    }
}
