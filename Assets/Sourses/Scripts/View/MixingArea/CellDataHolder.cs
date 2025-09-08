using System;
using System.Collections.Generic;
using UnityEngine;

public class CellDataHolder : MonoBehaviour
{
    private List<ProjectileCell> _cells = new List<ProjectileCell>();

    public event Action<List<ProjectileCell>> CellsChanged;

    public void AddCell(ProjectileCell cell)
    {
        foreach (ProjectileCell item in _cells)
        {
            if(cell.Type == item.Type)
            {
                if(cell.Count > item.Count)
                {
                    _cells.Remove(item);
                    _cells.Add(cell);
                }

                break;
            }
        }

        _cells.Add(cell);

        CellsChanged?.Invoke(_cells);
    }

    public void Clear()
    {
        _cells.Clear();
    }
}
