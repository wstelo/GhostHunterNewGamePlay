using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private ProjectileButtonDragHandler _dragHandler;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    public ElementTypes Type { get; private set; }
    public int Count { get; private set; }
    public Color Color { get; private set; }

    public event Action<UnitPlatform, UnitButton> PlatformDetected;
    public event Action<MixingArea, UnitButton> MixingAreaDetected;

    private void Awake()
    {
        _dragHandler.MixingAreaDetected += DetectMixingViewArea;
        _dragHandler.PlatformDetected += DetectPlatform;
    }

    private void DetectPlatform(UnitPlatform platform)
    {

        PlatformDetected?.Invoke(platform, this);
    }

    private void DetectMixingViewArea(MixingArea area)
    {
        MixingAreaDetected?.Invoke(area, this);
    }

    public void Init(ProjectileCell projectileCell)
    {
        gameObject.SetActive(true);
        Type = projectileCell.Type;
        Count = projectileCell.Count;
        Color = projectileCell.Color;

        _text.text = $"{Count}";
        _image.color = Color;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
