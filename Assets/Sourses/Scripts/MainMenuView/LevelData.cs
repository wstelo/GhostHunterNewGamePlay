using Newtonsoft.Json;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class LevelData : ISelectable
{
    private TextAsset _jsonConfig;

    public LevelData(TextAsset jsonConfig)
    {
        _jsonConfig = jsonConfig;
        Config = GetLevelConfig();
    }

    public LevelConfig Config { get; private set; }
    public bool IsCompleted { get; private set; } = false;

    public int Number => Config.LevelNumber;

    public void Complete()
    {
        IsCompleted = true;
    }

    private LevelConfig GetLevelConfig()
    {
        string json = _jsonConfig.text;
        LevelConfig levelConfig = JsonConvert.DeserializeObject<LevelConfig>(json);

        return levelConfig ?? throw new System.Exception("Failed deserialization from JSON");
    }
}
