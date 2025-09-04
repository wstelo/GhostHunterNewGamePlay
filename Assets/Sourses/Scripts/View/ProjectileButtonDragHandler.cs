using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileButtonDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private LayerMask _platformLayer;
    [SerializeField] private LayerMask _mixingAreaLayer;

    private Vector2 _defaultPosition;
    private RectTransform _currentObjectTransform;
    private UnitPlatform _currentPlatform;
    private MixingArea _currentMixingArea;

    private OverlapHandler _overlapHandler;

    public event Action<UnitPlatform> PlatformDetected;
    public event Action<MixingArea> MixingAreaDetected;

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

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_currentObjectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            _currentObjectTransform.position = globalMousePosition;
        }

        _currentMixingArea = _overlapHandler.TryGetOverlappedView(eventData, _mixingAreaLayer);
        _currentPlatform = _overlapHandler.TryGetOverlappedPlatform(eventData, _mixingAreaLayer);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentPlatform != null)
        {
            PlatformDetected?.Invoke(_currentPlatform);
        }
        else if (_currentMixingArea != null)
        {
            MixingAreaDetected?.Invoke(_currentMixingArea);
        }
        else
        {
            _currentObjectTransform.DOMove(_defaultPosition, 0.5f);
        }
    }
}
