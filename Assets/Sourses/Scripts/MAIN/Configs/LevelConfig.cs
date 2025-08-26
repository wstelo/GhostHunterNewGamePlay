using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class LevelConfig
{
    public List<EnemiesLevelConfig> EnemiesLevelConfig;
    public int LevelNumber;
    public float LevelSpeed;

    public LevelConfig(List<EnemiesLevelConfig> enemiesConfig, int levelNumber, float levelSpeeed)
    {
        EnemiesLevelConfig = enemiesConfig;
        LevelNumber = levelNumber;
        LevelSpeed = levelSpeeed;
    }
}
