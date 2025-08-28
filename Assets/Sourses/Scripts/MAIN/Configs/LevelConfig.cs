using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class LevelConfig
{
    public List<EnemiesLevelConfig> EnemiesLevelConfigs;
    public int LevelNumber;
    public float LevelSpeed;

    public LevelConfig(List<EnemiesLevelConfig> enemiesConfig, int levelNumber, float levelSpeeed)
    {
        EnemiesLevelConfigs = enemiesConfig;
        LevelNumber = levelNumber;
        LevelSpeed = levelSpeeed;
    }
}
