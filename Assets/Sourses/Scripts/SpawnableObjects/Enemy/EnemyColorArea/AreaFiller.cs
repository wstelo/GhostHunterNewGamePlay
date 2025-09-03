using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaFiller : MonoBehaviour
{
    [SerializeField] private Image _mainArea;
    [SerializeField] private Image _frame;

    [Range(0, 1)] private float _fillAreaValue = 0.1f;

    public float AreaMinValue = 0f;
    public float AreaMaxValue = 1f;

    private void Awake()
    {
        _mainArea.type = Image.Type.Filled;
        _frame.type = Image.Type.Filled;

        _fillAreaValue = Mathf.Clamp(_fillAreaValue, AreaMinValue, AreaMaxValue);

        _mainArea.fillAmount = _fillAreaValue;
        _frame.fillAmount = _fillAreaValue;
    }

    public void Initialize(float fillValue, Color color)
    {
        _fillAreaValue = Mathf.Clamp(fillValue, AreaMinValue, AreaMaxValue);

        _mainArea.fillAmount = _fillAreaValue;
        _mainArea.color = color;
        _frame.fillAmount = _fillAreaValue;
        _frame.color = color;
    }
}
