using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class JsonSaver
{
    public static void SaveToJsonFile(LevelConfig levelConfig)
    {
        string json = JsonConvert.SerializeObject(levelConfig);

        string path = EditorUtility.SaveFilePanel("Save Level Config", "", $"level_config {levelConfig.LevelNumber}", "json");

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, json);
            Debug.Log("Config saved to: " + path);
        }
    }

    public static LevelConfig LoadFromJsonFile()
    {
        string path = EditorUtility.OpenFilePanel("LoadLevelConfig", "", "json");

        if (string.IsNullOrEmpty(path) == false)
        {
            string json = File.ReadAllText(path);

            Debug.Log("Config loaded from: " + path);

            LevelConfig config = JsonConvert.DeserializeObject<LevelConfig>(json);

            return config;
        }

        return null;
    }


    public static void SaveToJson(ElementTypes[,] buttonValues, int levelNumber)
    {
        string json = JsonConvert.SerializeObject(buttonValues);

        string path = EditorUtility.SaveFilePanel("Save Level Config", "", $"level_config {levelNumber}", "json");

        if (string.IsNullOrEmpty(path) == false)
        {
            File.WriteAllText(path, json);
            Debug.Log("Config saved to: " + path);
        }
    }

    public static string LoadFromJson()
    {
        string path = EditorUtility.OpenFilePanel("LoadLevelConfig", "", "json");

        if (string.IsNullOrEmpty(path) == false)
        {
            string json = File.ReadAllText(path);

            Debug.Log("Config loaded from: " + path);

            return json;
        }

        return null;
    }
}
