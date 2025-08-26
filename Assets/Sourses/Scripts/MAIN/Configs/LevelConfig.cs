using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class LevelConfig
{
    public ElementTypes[,] ButtonValues;
    public int LevelNumber;
    public float LevelSpeed;

    public LevelConfig(ElementTypes[,] buttonValues, int levelNumber, float levelSpeeed)
    {
        ButtonValues = buttonValues;
        LevelNumber = levelNumber;
        LevelSpeed = levelSpeeed;
    }
}
