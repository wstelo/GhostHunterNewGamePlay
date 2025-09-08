using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileButtonDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] protected LayerMask _cellLayer;

    protected OverlapHandler _overlapHandler;
    private Vector2 _defaultPosition;
    private RectTransform _currentObjectTransform;
    private CellDataHolder _currentCellDataHolder;

    public event Action<CellDataHolder> DataHolderDetected;

    private void Awake()
    {
        _overlapHandler = new OverlapHandler();
        _currentObjectTransform = transform as RectTransform;
        _defaultPosition = _currentObjectTransform.position;

        if (_defaultPosition != null && _currentObjectTransform != null)
        {
            _currentObjectTransform.position = _defaultPosition;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_currentObjectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            _currentObjectTransform.position = globalMousePosition;
        }        
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _currentCellDataHolder = _overlapHandler.TryGetCellDataHolder(eventData, _cellLayer);

        if (_currentCellDataHolder != null)
        {
            DataHolderDetected?.Invoke(_currentCellDataHolder);
        }

        _currentObjectTransform.DOMove(_defaultPosition, 0.5f);
    }
}
