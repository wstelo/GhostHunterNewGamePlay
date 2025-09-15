using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileButtonView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;

    private string _defaultTextCount = "";
    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = _image.color;
    }

    public void Init(int count)
    {
        _countText.text = $"{count}";
    }

    public void Init(int count, Color color)
    {
        _countText.text = $"{count}";
        _image.color = color;
    }

    public void ResetCount()
    {
        _countText.text = _defaultTextCount;
        _image.color = _defaultColor;
    }
}
