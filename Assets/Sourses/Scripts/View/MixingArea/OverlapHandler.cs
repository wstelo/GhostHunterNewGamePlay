using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverlapHandler
{
    private List<RaycastResult> _raycastResult = new List<RaycastResult>();
    private Ray _ray;
    private RaycastHit[] _hit = new RaycastHit[5];
    private float _maxDistance = 100f;

    public CellDataHolder TryGetCellDataHolder(PointerEventData eventData, LayerMask mask)
    {
        CellDataHolder holder = TryGetOverlappedView(eventData, mask);

        if (holder == null)
        {
            holder = TryGetOverlappedPlatform(eventData, mask);
        }

        return holder;
    }

    private CellDataHolder TryGetOverlappedView(PointerEventData eventData, LayerMask mixingAreaMask)
    {
        EventSystem.current.RaycastAll(eventData, _raycastResult);

        foreach (var item in _raycastResult)
        {
            if (mixingAreaMask.IsContains(item.gameObject.layer))
            {
                if (item.gameObject.TryGetComponent(out CellDataHolder area))
                {
                    return area;
                }
            }
        }

        return null;
    }

    private CellDataHolder TryGetOverlappedPlatform(PointerEventData eventData, LayerMask platformMask)
    {
        _ray = Camera.main.ScreenPointToRay(eventData.position);

        int hitCount = Physics.RaycastNonAlloc(_ray, _hit, _maxDistance, platformMask);

        if (hitCount > 0)
        {
            foreach (var item in _hit)
            {
                if (item.collider.TryGetComponent(out CellDataHolder platform))
                {
                    return platform;
                }
            }
        }

        return null;
    }
}
