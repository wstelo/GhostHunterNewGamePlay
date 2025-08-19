using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChapterConfig", menuName = "NewChapterConfig / NewChapter")]
public class ChapterConfig : ScriptableObject
{
    [SerializeField] private int _number;
    [SerializeField] private ChapterPreviewPrefab _prefabForMenu;
    [SerializeField] private int _levelsCount = 10;
    [SerializeField] private List<TextAsset> _jsonLevelConfigs = new List<TextAsset>();

    public ChapterPreviewPrefab PrefabForMenu => _prefabForMenu;
    public List<TextAsset> LevelConfigs => _jsonLevelConfigs;
    public int Number => _number;

    private void OnValidate()
    {
        while (_jsonLevelConfigs.Count < _levelsCount)
        {
            _jsonLevelConfigs.Add(null);
        }

        while (_jsonLevelConfigs.Count > _levelsCount)
        {
            _jsonLevelConfigs.RemoveAt(_jsonLevelConfigs.Count -1);
        }
    }
}
