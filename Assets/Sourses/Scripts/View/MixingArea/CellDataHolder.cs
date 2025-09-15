using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class CellDataHolder : MonoBehaviour
{
    public event Action<MultiProjectileCell> CellChanged;
 
    public void AddCell(MultiProjectileCell cell)
    {
        if (cell == null)
        {
            return;
        }
        else
        {
            CellChanged?.Invoke(cell);
        }
    }
}
