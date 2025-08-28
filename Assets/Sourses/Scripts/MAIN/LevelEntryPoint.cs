using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Splines;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BuildPreviewer _buildPreviewer;
    [SerializeField] private EnemySpawnPointDetector _spawnDetector;
    [SerializeField] private ProjectileButtonHandler _projectileButtonHandler;
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private TextAsset _jsonLevelConfig;

    private SpawnableObjectDataFactory _spawnableObjectDataFactory;
    private List<DefenderData> _defendersData = new List<DefenderData>();
    private List<EnemyData> _enemiesData = new List<EnemyData>();
    private List<ProjectileData> _projectilesData = new List<ProjectileData>();


    private SpawnerHandler _unitSpawnerHandler;
    private DefenderSpawnHandler _defenderSpawnHandler;
    private EnemySpawnHandler _enemySpawnHandler;
    private DefenderBuilder _buildConstructor;

    private CellHandler _cellHandler;
    private UnitViewHandler _unitViewHandler;

    private List<ElementTypes> _elementTypesOnLevel;
    private LevelConfig _levelConfig;

    [Inject] private ConfigsRepository _configRepository;

    [Inject] private DefenderConfig _defenderConfig;

    private void Awake()
    {
        _levelConfig = GetLevelConfig();
        _elementTypesOnLevel = GetCurrentElementTypes(_levelConfig);
        _spawnableObjectDataFactory = new SpawnableObjectDataFactory(_configRepository);
        _defendersData = _spawnableObjectDataFactory.GetDefendersData();
        _projectilesData = _spawnableObjectDataFactory.GetProjectilesData();
        _enemiesData = _spawnableObjectDataFactory.GetEnemiesData();

        _cellHandler = new CellHandler(_enemySpawnHandler, _elementTypesOnLevel, _configRepository.ConfigList);
        _unitViewHandler = new UnitViewHandler(_cellHandler, _projectileButtonHandler);
        _buildConstructor = new DefenderBuilder(_inputHandler, _buildPreviewer);

        _unitSpawnerHandler = new SpawnerHandler(_enemiesData, GetCurrentLevelDefenderData(_defenderConfig), _projectilesData, _configRepository.ConfigList);
        _enemySpawnHandler = new EnemySpawnHandler(_levelConfig, _unitSpawnerHandler, _spline, _enemiesData, _spawnDetector);

        _defenderSpawnHandler = new DefenderSpawnHandler(_unitViewHandler, _buildConstructor, _unitSpawnerHandler, _defendersData.First());
    }

    private DefenderData GetCurrentLevelDefenderData(DefenderConfig defenderConfig)
    {
        return new DefenderData(defenderConfig.DefenderType, defenderConfig.Prefab, defenderConfig.UnitPreviewPrefab, defenderConfig.HitEffect);
    }

    private List<ElementTypes> GetCurrentElementTypes(LevelConfig levelConfig)
    {
        List<ElementTypes> elements = new List<ElementTypes>();

        foreach (var item in levelConfig.EnemiesLevelConfigs)
        {
            if (elements.Contains(item.ElementType) == false)
            {
                elements.Add(item.ElementType);
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
