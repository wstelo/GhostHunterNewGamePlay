using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    public ElementTypes Type { get; private set; }
    public int Count { get; private set; }
    public Color Color { get; private set; }

    public event Action<ProjectileButton> ButtonClicked;
    public event Action<ProjectileButton> ButtonDestroyed;

    private void OnEnable()
    {
        _button.onClick.AddListener(ClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ClickButton);
    }

    private void OnDestroy()
    {
        ButtonDestroyed?.Invoke(this);
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

    private void ClickButton()
    {
        ButtonClicked?.Invoke(this);
    }
}
