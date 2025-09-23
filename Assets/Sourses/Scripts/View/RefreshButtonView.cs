using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButtonView : MonoBehaviour
{
    [SerializeField] private Button _mainButton;
    [SerializeField] private Image _fillArea;

    [Range(0f, 1f)] private float _fillAmount = 1f;

    public event Action Clicked;

    public float FillAmountValue => _fillAmount;
    public float FillAmountMaxValue { get; private set; } = 1f;

    private void Awake()
    {
        _fillArea.fillAmount = _fillAmount;
        _mainButton.onClick.AddListener(ClickButton);
    }

    public void FillArea(float value)
    {
        if(value <= 0f)
        {
            _fillArea.fillAmount = 0;
        }

        _fillArea.fillAmount = Mathf.Clamp01(value);
    }

    private void ClickButton()
    {
        Clicked?.Invoke();
    }
}
