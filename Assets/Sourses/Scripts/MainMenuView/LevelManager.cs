using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<ChapterConfig> _chapterConfigs = new List<ChapterConfig>();
    [SerializeField] private SelectorViewPanel _chapterSelectorPanel;
    [SerializeField] private SelectorViewPanel _levelSelectorPanel;
    [SerializeField] private MainMenuButtonHandler _mainMenuButtonHandler;

    private List<ChapterData> _chapters = new List<ChapterData>();
    private LevelSelector _chapterSelector;
    private SceneLoader _sceneLoader;

    public GameData GameData;

    private void Awake()
    {
        _chapters = SetChapters(_chapterConfigs);
        _chapterSelector = new LevelSelector(_chapterSelectorPanel, _levelSelectorPanel, _chapters);
        _mainMenuButtonHandler.StartButtonClicked += StartCurrentLevel;
        _sceneLoader = new SceneLoader();
    }

    private List<ChapterData> SetChapters(List<ChapterConfig> configs)
    {
        List<ChapterData> chapters = new List<ChapterData>();

        foreach (var chapter in configs)
        {
            chapters.Add(new ChapterData(chapter.PrefabForMenu, chapter.LevelConfigs, chapter.Number));
        }

        return chapters;
    }

    private void StartCurrentLevel()
    {
        LevelData data = _chapterSelector.GetCurrentLevelData();
        GameData.SetCurrentLevelData(data) ;
        StartCoroutine(SwitchScene("FirstLevel 1"));
    }

    private IEnumerator SwitchScene(string sceneName)
    {
        yield return _sceneLoader.LoadScene(sceneName);
    }
}
