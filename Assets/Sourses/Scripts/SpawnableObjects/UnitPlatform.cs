using UnityEngine;
using TMPro;

public class UnitPlatform : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;

    private Defender _currentDefender;
    private int _defaultValue = 0;


    public bool IsEmpty { get; private set; } = true;

    private void Awake()
    {
        _countText.text = _defaultValue.ToString();
    }

    public void Occupy(Defender currentDefender)
    {
        IsEmpty = false;
        _currentDefender = currentDefender;
        _currentDefender.Disabled += Clear;
        RefreshCountPanel(_currentDefender.ProjectileContainer.ProjectileCount);
        _currentDefender.ProjectileContainer.CountChanged += RefreshCountPanel;
    }

    public void Clear(Defender currentDefender)
    {
        IsEmpty = true;
        RefreshCountPanel(_defaultValue);
        _currentDefender.ProjectileContainer.CountChanged -= RefreshCountPanel;
        _currentDefender.Disabled -= Clear;
        _currentDefender = null;
    }

    private void RefreshCountPanel(int count)
    {
        _countText.text = count.ToString();
    }
}
