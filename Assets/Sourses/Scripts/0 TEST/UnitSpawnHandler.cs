using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnHandler
{
    private UnitViewHandler _unitViewHandler;
    private UnitPreviewer _unitPreviewer;
    private LevelConfig _levelConfig;
    private List<SpawnableObjectData<Ghost>> _unitsData = new List<SpawnableObjectData<Ghost>>();

    public UnitSpawnHandler(UnitViewHandler unitViewHandler, UnitPreviewer unitPreviewer, List<SpawnableObjectData<Ghost>> unitsData)
    {
        _unitViewHandler = unitViewHandler;
        _unitViewHandler.ButtonClicked += SetPreview;
        _unitPreviewer = unitPreviewer;
        _unitsData = unitsData;
    }

    private void SetPreview(ProjectileButton button)
    {
        foreach (var item in _unitsData)
        {
            if(item.Type == button.Type)
            {
                Debug.Log("Bum");
                _unitPreviewer.Activate(item.PreviewPrefab, item.Color);
            }
        }
    }

}
