using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    public event Action StartButtonClicked;

    private void Awake()
    {
        _startGameButton.onClick.AddListener(ClickStartButton);
    }

    private void ClickStartButton()
    {
        StartButtonClicked?.Invoke();
    }
}
