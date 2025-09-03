using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiColorAreaGenerator : MonoBehaviour
{
    [SerializeField] private AreaFiller _areaFillerPrefab;
    [SerializeField] private Transform _parentCanvas;

    private float _maxFillValue;
    private float _maxCirlceDegrees = 360;

    public void Init(List<Color> colors)
    {
        _maxFillValue = _areaFillerPrefab.AreaMaxValue;
        int sectorsCount = colors.Count;
        float reqiredFillValue = _maxFillValue / sectorsCount;
        float requiredDegreesStep = _maxCirlceDegrees / sectorsCount;
        float currentRotation = 0f;

        foreach (var color in colors)
        {
            AreaFiller currentArea = Instantiate(_areaFillerPrefab);
            currentArea.transform.SetParent(_parentCanvas, false);

            currentArea.transform.localPosition = Vector3.zero;
            currentArea.transform.localScale = Vector3.one;

            currentArea.Initialize(reqiredFillValue, color);
            currentArea.transform.localRotation = Quaternion.Euler(0,0, currentRotation);
            currentRotation += requiredDegreesStep;
        }
    }
}
