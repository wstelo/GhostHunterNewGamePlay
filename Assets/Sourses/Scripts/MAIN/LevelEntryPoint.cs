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
    [SerializeField] private ProjectileButtonHolder _projectileButtonHandler;
    [SerializeField] private RefreshButtonHandler _refreshButtonHandler;
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private TextAsset _jsonLevelConfig;
    [SerializeField] private GraveTypeHandler _graveTypeHandler;
    [SerializeField] private UnitPlatformHandler _unitPlatformHandler;

    private float _distanceBetweenEnemies = GameStaticData.DistanceBetweenEnemies;                                   ////////////////////////////////////////////////
    private EnemySpawnHandler _enemySpawnHandler;
    private TestCellHandler _cellHandler;
    private UnitViewHandler _unitViewHandler;
    private DefenderSpawnHandler _defenderSpawnHandler;

    private LevelConfig _levelConfig;


    private ElementColorizer _elementColorizer;

    [Inject] private SpawnersHandler _unitSpawnerHandler;
    [Inject] private ConfigsRepository _configRepository;
    [Inject] private DefenderConfig _defenderConfig;

    private void Awake()
    {
        _levelConfig = GetLevelConfig();
        _enemySpawnHandler = new EnemySpawnHandler(_levelConfig, _unitSpawnerHandler, _splineContainer, _distanceBetweenEnemies);
        _defenderSpawnHandler = new DefenderSpawnHandler(_unitSpawnerHandler, _defenderConfig);
        _unitPlatformHandler.Initialize(_defenderSpawnHandler);
        _graveTypeHandler.Init(_levelConfig);


        _elementColorizer = new ElementColorizer(_configRepository.ElementConfigs); ////////////////////////////////////////////// использовать Colorizer где можно
        _cellHandler = new TestCellHandler(_levelConfig, _configRepository.ElementConfigs, _enemySpawnHandler, _projectileButtonHandler.ButtonCount, _elementColorizer);
        _unitViewHandler = new UnitViewHandler(_cellHandler, _projectileButtonHandler, _refreshButtonHandler);
   
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
