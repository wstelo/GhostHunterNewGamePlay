using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterData : ISelectable
{
    private List<LevelData> _levels = new List<LevelData>();
    private ChapterPreviewPrefab _prefabForMenu;
    private int _number;
    private bool _isCompleted = false;

    public ChapterData(ChapterPreviewPrefab prefabForMenu, List<TextAsset> jsonConfig, int number)
    {
        _prefabForMenu = prefabForMenu;
        _number = number;
        Init(jsonConfig);
    }

    public bool IsCompleted => _isCompleted;
    public ChapterPreviewPrefab Prefab => _prefabForMenu;
    public List<LevelData> Levels => _levels;
    public int Number => _number;

    public void CompleteLevel()
    {
        _isCompleted = true;
    }

    private void Init(List<TextAsset> configs)
    {
        foreach (var config in configs)
        {
            _levels.Add(new LevelData(config));
        }
    }
}
