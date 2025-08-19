using System;
using System.Collections.Generic;

public class Selector<T> where T : ISelectable
{
    private SelectorViewPanel _selectionPanel;
    private List<T> _selectableObjects = new List<T>();
    private int _minSelectionLevelNumber = 1;
    private int _selectedObjectIndex = 0;

    public event Action<T> SelectionChanged;

    public Selector(SelectorViewPanel levelSelectionPanel, List<T> selectableObjects)
    {
        _selectionPanel = levelSelectionPanel;
        _selectionPanel.LeftLevelSelectionButtonClicked += DecreaseCurrentLevel;
        _selectionPanel.RightLevelSelectionButtonClicked += IncreaseCurrentLevel;

        Init(selectableObjects);
    }

    public void Init(List<T> selectableObjects)
    {
        _selectableObjects = selectableObjects;
        _selectedObjectIndex = GetFirstUncompletedIndex(_selectableObjects);
        UpdateView();
    }

    public T GetCurrentSelectedObject()
    {
        return _selectableObjects[_selectedObjectIndex];
    }

    private int GetFirstUncompletedIndex(List<T> selectableObjects)
    {
        for (int i = 0; i < selectableObjects.Count; i++)
        {
            if (selectableObjects[i].IsCompleted == false)
            {
                return i;
            }
        }
        return selectableObjects.Count - 1;
    }

    private void IncreaseCurrentLevel()
    {
        if (_selectedObjectIndex + 1 < _selectableObjects.Count)
        {
            _selectedObjectIndex++;
        }

        UpdateView();
    }

    private void DecreaseCurrentLevel()
    {
        if (_selectedObjectIndex + 1 > _minSelectionLevelNumber)
        {
            _selectedObjectIndex--;
        }

        UpdateView();
    }

    private void UpdateView()
    {
        if (_selectedObjectIndex == 0)
        {
            _selectionPanel.DisableLeftButton();
        }
        else
        {
            _selectionPanel.EnableLeftButton();
        }

        if (_selectedObjectIndex == _selectableObjects.Count - 1)
        {
            _selectionPanel.DisableRightButton();
        }
        else
        {
            _selectionPanel.EnableRightButton();
        }

        _selectionPanel.ShowInfo(_selectableObjects[_selectedObjectIndex].Number, _selectableObjects.Count);
        SelectionChanged?.Invoke(_selectableObjects[_selectedObjectIndex]);
    }
}
