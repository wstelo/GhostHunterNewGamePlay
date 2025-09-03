using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class ProjectileButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private LayerMask _platformLayer;
    [SerializeField] private LayerMask _mixingAreaLayer;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    private RectTransform _currentObjectTransform;
    private Vector2 _defaultPosition;
    private List<RaycastResult> _raycastResult = new List<RaycastResult>();

    private UnitPlatform _currentPlatform;
    private DefenderMixingArea _mixingArea;
    private Ray _ray;
    private RaycastHit[] _hit = new RaycastHit[5];
    private float _maxDistance = 100f;

    public ElementTypes Type { get; private set; }
    public int Count { get; private set; }
    public Color Color { get; private set; }

    public event Action<UnitPlatform, ProjectileButton> PlatformDetected;

    public event Action<ProjectileButton> ButtonClicked; ///////////////////////////////
    public event Action<ProjectileButton> ButtonDestroyed;

    private void Awake()
    {
        _currentObjectTransform = transform as RectTransform;
        _defaultPosition = _currentObjectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(_currentObjectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            _currentObjectTransform.position = globalMousePosition;
        }

        _mixingArea = TryGetViewOverlap(eventData);
        _currentPlatform = TryGetPlatformOverlap(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentPlatform != null)
        {
            PlatformDetected?.Invoke(_currentPlatform, this);
        }

        _currentObjectTransform.DOMove(_defaultPosition, 0.5f);
    }

    private UnitPlatform TryGetPlatformOverlap(PointerEventData eventData)
    {
        _ray = Camera.main.ScreenPointToRay(eventData.position);

        int hitCount = Physics.RaycastNonAlloc(_ray, _hit, _maxDistance, _platformLayer);

        if (hitCount > 0)
        {
            foreach (var item in _hit)
            {
                if(item.collider.TryGetComponent(out UnitPlatform platform))
                {
                    Debug.Log("Platform");
                    return platform;
                }
            }
        }

        return null;
    }

    private DefenderMixingArea TryGetViewOverlap(PointerEventData eventData)
    {
        EventSystem.current.RaycastAll(eventData, _raycastResult);

        foreach (var item in _raycastResult)
        {
            if (_mixingAreaLayer.IsContains(item.gameObject.layer))
            {
                if (item.gameObject.TryGetComponent(out DefenderMixingArea area))
                {
                    Debug.Log("Area");
                    return area;
                }
            }
        }

        return null;
    }

    private void OnDestroy()
    {
        ButtonDestroyed?.Invoke(this);
    }

    public void Init(ProjectileCell projectileCell)
    {
        gameObject.SetActive(true);
        Type = projectileCell.Type;
        Count = projectileCell.Count;
        Color = projectileCell.Color;

        _text.text = $"{Count}";
        _image.color = Color;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
