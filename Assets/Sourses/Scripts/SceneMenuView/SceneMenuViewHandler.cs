using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneMenuViewHandler : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeGameButton;
    [SerializeField] private Button _quitToMenuButton;
    [SerializeField] private inputHandler _inputHandler;
    [SerializeField] private Image _pauseView;

    private SceneLoader _sceneLoader;

    public event Action PauseButtonClicked;
    public event Action ResumeButtonClicked;

    private void Awake()
    {
        _sceneLoader = new SceneLoader();
    }

    private void OnEnable()
    {
        _pauseView.gameObject.SetActive(false);
        _pauseButton.onClick.AddListener(PauseButtonClick);
        _resumeGameButton.onClick.AddListener(ResumeButtonClick);
        _quitToMenuButton.onClick.AddListener(QuitToMainMenu);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(PauseButtonClick);
        _resumeGameButton.onClick.RemoveListener(ResumeButtonClick);
    }

    private void PauseButtonClick()
    {
        _pauseView.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ResumeButtonClick()
    {
        _pauseView.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(SwitchScene("MainMenu"));
    }

    private IEnumerator SwitchScene(string sceneName)
    {
        yield return _sceneLoader.LoadScene(sceneName);
    }
}
