using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellDataHolder : MonoBehaviour
{
    private Dictionary<ElementTypes, ProjectileCell> _cells = new Dictionary<ElementTypes, ProjectileCell>();

    public event Action<List<ProjectileCell>> CellsChanged;

    public List<ProjectileCell> GetCurrentCells()
    {
        return _cells.Values.ToList();
    }

    public void AddCells(List<ProjectileCell> cells)
    {
        bool isChanged = false;

        if(cells == null || cells.Count == 0)
        {
            return;
        }

        if(_cells.Count == 0)
        {
            foreach (ProjectileCell cell in cells)
            {
                _cells.Add(cell.ElementType, cell);
                isChanged = true;
            }
        }
        else
        {
            foreach (ProjectileCell cell in cells)
            {
                if(_cells.TryGetValue(cell.ElementType, out ProjectileCell currentCell))
                {
                    if(cell.Count > currentCell.Count)
                    {
                        _cells[currentCell.ElementType] = cell;
                        isChanged = true;
                    }
                }
                else
                {
                    _cells.Add(cell.ElementType, cell);
                    isChanged = true;
                }
            }
        }

        if(isChanged == true)
        {
            CellsChanged?.Invoke(_cells.Values.ToList());
        }
    }

    public void AddCells(ProjectileCell cell)
    {
        bool isChanged = false;

        if (_cells.Count == 0)
        {
            _cells.Add(cell.ElementType, cell);
            isChanged = true;
        }
        else
        {           
            if(_cells.TryGetValue(cell.ElementType, out ProjectileCell currentCell))
            {
                if (cell.Count > currentCell.Count)
                {
                    _cells[currentCell.ElementType] = cell;
                    isChanged = true;
                }
            }
            else
            {              
                _cells.Add(cell.ElementType, cell);
                isChanged = true;
            }
        }

        if (isChanged == true)
        {
            CellsChanged?.Invoke(_cells.Values.ToList());
        }
    }

    public void Clear()
    {
        _cells.Clear();
    }
}
