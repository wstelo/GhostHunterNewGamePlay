using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargingProjectileCell : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    private Color _defaultColor;
    public ElementTypes Type { get; private set; }
    public int Count { get; private set; }
    public Color Color { get; private set; }

    public bool IsActive { get; private set; }

    public void Init(ElementTypes type, int count, Color color)
    {
        _defaultColor = _image.color;
        Type = type;
        Count = count;
        Color = color;

        _text.text = $"{Count}";
        _image.color = Color;
        Activate();
    }

    public void DecreaseCount()
    {
        Count--;
        _text.text = $"{Count}";

        if (Count == 0)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        Count = 0;
        Color = _defaultColor;
        gameObject.SetActive(false);
        IsActive = false;
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        IsActive = true;
    }
}
