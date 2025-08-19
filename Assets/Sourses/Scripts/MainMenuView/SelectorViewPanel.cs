using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectorViewPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private Button _leftSelectionButton;
    [SerializeField] private Button _rightSelectionButton;

    public event Action LeftLevelSelectionButtonClicked;
    public event Action RightLevelSelectionButtonClicked;

    private void OnEnable()
    {
        _leftSelectionButton.onClick.AddListener(LeftSelectionButtonClick);
        _rightSelectionButton.onClick.AddListener(RightSelectionButtonClick);
    }

    private void OnDisable()
    {
        _leftSelectionButton.onClick.RemoveListener(LeftSelectionButtonClick);
        _rightSelectionButton.onClick.RemoveListener(RightSelectionButtonClick);
    }

    public void EnableLeftButton()
    {
        _leftSelectionButton.gameObject.SetActive(true);
    }

    public void EnableRightButton()
    {
        _rightSelectionButton.gameObject?.SetActive(true);
    }

    public void DisableLeftButton()
    {
        _leftSelectionButton.gameObject.SetActive(false);
    }

    public void DisableRightButton()
    {
        _rightSelectionButton.gameObject?.SetActive(false);
    }

    public void ShowInfo(int currentValue, int maxValue)
    {
        _levelNumber.text = $"{currentValue} / {maxValue}";
    }

    private void LeftSelectionButtonClick()
    {
        LeftLevelSelectionButtonClicked?.Invoke();
    }

    private void RightSelectionButtonClick()
    {
        RightLevelSelectionButtonClicked?.Invoke();
    }
}
