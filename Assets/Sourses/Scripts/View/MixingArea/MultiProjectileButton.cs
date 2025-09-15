using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MultiProjectileButton : MonoBehaviour
{
    [SerializeField] private ProjectileButtonView _textView;
    [SerializeField] private ProjectileButtonDragHandler _dragHandler;
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;
    [SerializeField] private CellDataHolder _cellDataHolder;

    private MultiProjectileCell _currentCell;
    private int _tempCount = 0;
    private List<ElementTypes> _tempElements = new List<ElementTypes>();
    private List<Color> _tempColors = new List<Color>();

    private void Awake()
    {
        _dragHandler.DataHolderDetected += DetectDataHolder;                //////////////////////// otpiski
        _cellDataHolder.CellChanged += SetCurrentCell;
    }

    private void DetectDataHolder(CellDataHolder dataHolder)
    {
        dataHolder.AddCell(_currentCell);
    }

    private void SetCurrentCell(MultiProjectileCell cell)
    {
        bool isChanged = false;

        if (_currentCell == null)
        {
            _currentCell = cell;
            _currentCell.Consume();
        }
        else
        {
            _tempElements = _currentCell.ElementTypes;
            _tempColors = _currentCell.Colors;
            _tempCount = _currentCell.Count;

            for (int i = 0; i < cell.ElementTypes.Count; i++)
            {
                if (_currentCell.ElementTypes.Contains(cell.ElementTypes[i]) == false)
                {
                    _tempElements.Add(cell.ElementTypes[i]);
                    _tempColors.Add(cell.Colors[i]);
                    isChanged = true;
                }
            }

            if (cell.Count > _currentCell.Count)
            {
                _tempCount = cell.Count;
                isChanged = true;
            }

            if (isChanged == true)
            {
                _currentCell = new MultiProjectileCell(_tempElements, _tempCount, _tempColors, ResetHolder);
                cell.Consume();
            }
        }

        _textView.Init(_currentCell.Count);
        _colorGenerator.Init(_currentCell.Colors);
    }

    private void ResetHolder()
    {
        _currentCell = null;
        _colorGenerator.Clear();
        _textView.ResetCount();
    }
}
