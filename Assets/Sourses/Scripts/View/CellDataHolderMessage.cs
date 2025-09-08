using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDataHolderMessage : MonoBehaviour
{
    public CellDataHolder Sender {  get; private set; }
    public List<ProjectileCell> CurrentCells { get; private set; }

    public CellDataHolderMessage(CellDataHolder sender, List<ProjectileCell> currentCells)
    {
        Sender = sender;
        CurrentCells = currentCells;
    }
}
