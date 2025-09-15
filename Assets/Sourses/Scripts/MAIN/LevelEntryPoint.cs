using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEditor.Progress;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private EnemySpawnPointDetector _spawnDetector;
    [SerializeField] private ProjectileButtonHandler _projectileButtonHandler;
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private TextAsset _jsonLevelConfig;

    private EnemySpawnHandler _enemySpawnHandler;

    private CellHandler _cellHandler;
    private UnitViewHandler _unitViewHandler;

    private List<ElementTypes> _elementTypesOnLevel;
    private LevelConfig _levelConfig;

    [Inject] private SpawnersHandler _unitSpawnerHandler;
    [Inject] private ConfigsRepository _configRepository;
    [Inject] private DefenderConfig _defenderConfig;

    private void Awake()
    {
        _levelConfig = GetLevelConfig();
        _elementTypesOnLevel = GetCurrentElementTypes(_levelConfig);

        _cellHandler = new CellHandler(_elementTypesOnLevel, _configRepository.ConfigList);
        _unitViewHandler = new UnitViewHandler(_cellHandler, _projectileButtonHandler);
        
        _enemySpawnHandler = new EnemySpawnHandler(_levelConfig, _unitSpawnerHandler, _splineContainer, _spawnDetector);
    }

    //private DefenderConfig GetCurrentLevelDefenderData(DefenderConfig defenderConfig)                                                                                          /////////////////////////////////////////////////
    //{
    //    return new DefenderData(defenderConfig.DefenderType, defenderConfig.Prefab, defenderConfig.HitEffect);
    //}

    private List<ElementTypes> GetCurrentElementTypes(LevelConfig levelConfig)
    {
        List<ElementTypes> elements = new List<ElementTypes>();

        foreach (var enemyConfig in levelConfig.EnemiesLevelConfigs)
        {
            foreach (var element in enemyConfig.ElementTypes)
            {
                if (elements.Contains(element) == false)
                {
                    elements.Add(element);
                }
            }
        }

        return elements;
    }

    public void Init(LevelData currentLevel)
    {
        _levelConfig = currentLevel.Config;
    }

    private LevelConfig GetLevelConfig()
    {
        string json = _jsonLevelConfig.text;
        LevelConfig levelConfig = JsonConvert.DeserializeObject<LevelConfig>(json);

        return levelConfig ?? throw new System.Exception("Failed deserialization from JSON");
    }
}
