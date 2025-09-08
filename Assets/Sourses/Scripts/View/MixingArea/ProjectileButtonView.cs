using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileButtonView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;

    public void Init(int count, Color color)
    {
        _countText.text = $"{count}";
        _image.color = color;
    }
}
