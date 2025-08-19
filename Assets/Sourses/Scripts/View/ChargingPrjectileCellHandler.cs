using System.Collections.Generic;
using UnityEngine;

public class ChargingPrjectileCellHandler : MonoBehaviour
{
    [SerializeField] private List<ChargingProjectileCell> _cell;

    public int CellCount => _cell.Count;

    public void SetChargingProjectileCells(List<ChargingProjectileCell> cells)
    {
        if (cells.Count != CellCount)
        {
            throw new System.Exception(" оличество ProjectileCell не соответствует количеству слотов.");
        }

        for (int i = 0; i < _cell.Count; i++)
        {
            _cell[i].Init(cells[i].Type, cells[i].Count, cells[i].Color);
        }
    }

    public List<ChargingProjectileCell> GetChargingProjectileCells()
    {
        return _cell;
    }
}
