using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshButtonHandler : MonoBehaviour
{
    [SerializeField] private RefreshButtonView _refreshButtonView;
    [SerializeField] private float _refreshTime = 3f;                                               ///////////////////////////////// регулировка из EntryPoint или GameStaticData

    public event Action Clicked;

    public bool IsActive { get; private set; } = false;

    private void Awake()
    {
        _refreshButtonView.Clicked += Click;
        StartCoroutine(RefreshViewWithDelay(_refreshTime));
    }

    private void Click()
    {
        if (IsActive == true)
        {
            Clicked?.Invoke();
            StartCoroutine(RefreshViewWithDelay(_refreshTime));
            IsActive = false;
        }
    } 

    private IEnumerator RefreshViewWithDelay(float time)
    {
        float currentTime = 0f;

        while (currentTime < _refreshTime)
        {
            currentTime += Time.deltaTime;
            float progress = currentTime / _refreshTime;
            _refreshButtonView.FillArea(_refreshButtonView.FillAmountMaxValue - progress);
            yield return null;
        }

        _refreshButtonView.FillArea(0);
        IsActive = true;
    }
}
