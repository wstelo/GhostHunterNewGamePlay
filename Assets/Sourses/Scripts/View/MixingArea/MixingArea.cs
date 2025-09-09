using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public class MixingArea : MonoBehaviour
{
    [SerializeField] private TMP_Text _textView;
    [SerializeField] private ProjectileButtonDragHandler _dragHandler;
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;
    [SerializeField] private CellDataHolder _cellDataHolder;

    private void Awake()
    {
        _cellDataHolder.CellsChanged += Init;
        _dragHandler.DataHolderDetected += DetectDataHolder;
    }

    private void DetectDataHolder(CellDataHolder dataHolder)
    {
        dataHolder.AddCells(_cellDataHolder.GetCurrentCells());
    }

    private void Init(List<ProjectileCell> cells)
    {
        if (cells != null && cells.Count > 0)
        {
            int maxCount = cells.Max(cell => cell.Count);
            _textView.text = maxCount.ToString();
        }

        _colorGenerator.Init(cells);
    }
}
