using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverlapHandler 
{
    private List<RaycastResult> _raycastResult = new List<RaycastResult>();
    private Ray _ray;
    private RaycastHit[] _hit = new RaycastHit[5];
    private float _maxDistance = 100f;

    public MixingArea TryGetOverlappedView(PointerEventData eventData, LayerMask mixingAreaMask)
    {
        EventSystem.current.RaycastAll(eventData, _raycastResult);

        foreach (var item in _raycastResult)
        {
            if (mixingAreaMask.IsContains(item.gameObject.layer))
            {
                if (item.gameObject.TryGetComponent(out MixingArea area))
                {
                    return area;
                }
            }
        }

        return null;
    }

    public UnitPlatform TryGetOverlappedPlatform(PointerEventData eventData, LayerMask platformMask)
    {
        _ray = Camera.main.ScreenPointToRay(eventData.position);

        int hitCount = Physics.RaycastNonAlloc(_ray, _hit, _maxDistance, platformMask);

        if (hitCount > 0)
        {
            foreach (var item in _hit)
            {
                if (item.collider.TryGetComponent(out UnitPlatform platform))
                {
                    return platform;
                }
            }
        }

        return null;
    }
}
