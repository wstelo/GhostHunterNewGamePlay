using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class ProjectileButton : MonoBehaviour
{
    [SerializeField] private ProjectileButtonDragHandler _dragHandler;
    [SerializeField] private ProjectileButtonView _textView;

    private ProjectileCell _cells;

    public event Action Disabled;

    private void Awake()
    {
        _dragHandler.DataHolderDetected += DetectDataHolder;
    }

    private void DetectDataHolder(CellDataHolder dataHolder)
    {
        dataHolder.AddCell(new MultiProjectileCell(_cells.ElementType, _cells.Count, _cells.Color, Deactivate));
    }

    public void Init(ProjectileCell projectileCell)
    {
        gameObject.SetActive(true);
        _cells = projectileCell;                               /////////////////////////////////////////////// huinya?
        _textView.Init(_cells.Count, _cells.Color);
    }

    public void Deactivate()            
    {    
        gameObject.SetActive(false);
        Disabled?.Invoke();
    }

    private void OnDestroy()
    {
        _dragHandler.DataHolderDetected -= DetectDataHolder;
    }
}
