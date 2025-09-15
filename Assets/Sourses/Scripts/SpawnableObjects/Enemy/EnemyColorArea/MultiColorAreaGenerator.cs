using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MultiColorAreaGenerator : MonoBehaviour
{
    [SerializeField] private AreaFiller _areaFillerPrefab;
    [SerializeField] private Transform _parentCanvas;

    private float _maxFillValue;
    private float _maxCirlceDegrees = 360;
    private List<AreaFiller> _currentArea = new List<AreaFiller>();

    public void Clear()
    {
        foreach (AreaFiller areaFiller in _currentArea)
        {
            Destroy(areaFiller.gameObject);
        }

        _currentArea.Clear();
    }

    public void Init(List<Color> colors)
    {
       SetColor(colors);
    }

    public void Init(List<ProjectileCell> cells)
    {
        List<Color> colors = new List<Color>();

        foreach (ProjectileCell cell in cells)
        {
            if (colors.Contains(cell.Color) == false)
            {
                colors.Add(cell.Color);
            }
        }

        SetColor(colors);
    }

    private void SetColor(List<Color> colors)
    {

        if (colors.Count > 0)
        {
            _maxFillValue = _areaFillerPrefab.AreaMaxValue;
            int sectorsCount = colors.Count;
            float reqiredFillValue = _maxFillValue / sectorsCount;
            float requiredDegreesStep = _maxCirlceDegrees / sectorsCount;
            float currentRotation = 0f;

            if (_currentArea.Count > 0)
            {
                foreach (var item in _currentArea)
                {
                    Destroy(item.gameObject);                          ////////////////////////////////////// Сделать пул? Или?
                }

                _currentArea.Clear();
            }

            foreach (var color in colors)
            {
                AreaFiller areaFiller = Instantiate(_areaFillerPrefab);

                areaFiller.transform.SetParent(_parentCanvas, false);

                areaFiller.transform.localPosition = Vector3.zero;
                areaFiller.transform.localScale = Vector3.one;

                areaFiller.Initialize(reqiredFillValue, color);
                areaFiller.transform.localRotation = Quaternion.Euler(0, 0, currentRotation);
                currentRotation += requiredDegreesStep;

                _currentArea.Add(areaFiller);
            }
        }
    }
}
