using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector
{
    private SelectorViewPanel _chapterSelectionPanel;
    private SelectorViewPanel _levelSelectionPanel;
    private List<ChapterData> _chapters;
    private Selector<ChapterData> _chapterSelector;
    private Selector<LevelData> _gameLevelSelector;
    private ChapterData _currentChapter;
    private LevelData _currentLevel;
    private ChapterPreviewPrefab _currentPreview;

    public LevelSelector(SelectorViewPanel selectorViewPanel, SelectorViewPanel levelSelectionPanel, List<ChapterData> chapters)
    {
        
        _chapterSelectionPanel = selectorViewPanel;
        _levelSelectionPanel = levelSelectionPanel;
        _chapters = chapters;
        _chapterSelector = new Selector<ChapterData>(_chapterSelectionPanel, _chapters);

        InitChapterSelector();
    }

    public LevelData GetCurrentLevelData()
    {
        return _currentLevel;
    }

    private void InitChapterSelector()
    {
        _currentChapter = _chapterSelector.GetCurrentSelectedObject();
        SetCurrentChapterPreview(_currentChapter);
        _gameLevelSelector = new Selector<LevelData>(_levelSelectionPanel, _currentChapter.Levels);
        _currentLevel = _gameLevelSelector.GetCurrentSelectedObject();
        _chapterSelector.SelectionChanged += ResetCurrentChapter;
        _gameLevelSelector.SelectionChanged += ResetCurrentLevel;
    }

    private void ResetCurrentLevel(LevelData level)
    {
        _currentLevel = level;
    }

    private void ResetCurrentChapter(ChapterData chapter)
    {
        _currentChapter = chapter;
        SetCurrentChapterPreview(_currentChapter);
        _gameLevelSelector.Init(_currentChapter.Levels);
        _currentLevel = _gameLevelSelector.GetCurrentSelectedObject();
    }

    private void SetCurrentChapterPreview(ChapterData chapter)
    {
        if(_currentPreview != null)
        {
            GameObject.Destroy(_currentPreview.gameObject);
        }

        _currentPreview = GameObject.Instantiate(chapter.Prefab);
    }
}
