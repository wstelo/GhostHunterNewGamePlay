using System.Collections.Generic;
using UnityEngine;

public class ProjectileButtonHandler : MonoBehaviour
{
    [SerializeField] private List<UnitButton> _buttons;

    public int ButtonCount => _buttons.Count;

    public void SetButtons(List<ProjectileCell> cells)
    {
        if (cells.Count != _buttons.Count)
        {
            throw new System.Exception(" оличество ProjectileCell не соответствует количеству кнопок.");
        }

        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].Init(cells[i]);
        }
    }

    public List<UnitButton> GetProjectileButtons()
    {
        return _buttons;
    }
}
