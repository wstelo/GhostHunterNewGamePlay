using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Splines;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BuildPreviewer _buildPreviewer;
    [SerializeField] private EnemySpawnPointDetector _spawnDetector;
    [SerializeField] private ProjectileButtonHandler _projectileButtonHandler;
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private TextAsset _jsonLevelConfig;

    private List<DefenderData> _defendersData = new List<DefenderData>();
    private List<EnemyData> _enemiesData = new List<EnemyData>();
    private List<ProjectileData> _projectilesData = new List<ProjectileData>();

    private SpawnableObjectDataFactory _spawnableObjectDataFactory;

    private SpawnersHandler<Projectile> _projectileSpawnerHandler;
    private SpawnersHandler<Defender> _defenderSpawnerHandler;
    private SpawnersHandler<Enemy> _enemySpawnerHandler;
    private DefenderSpawnHandler _defenderSpawnHandler;
    private EnemySpawnHandler _enemySpawnHandler;

    private CellHandler _cellHandler;
    private UnitViewHandler _unitViewHandler;
    private DefenderBuilder _buildConstructor;

    private LevelConfig _levelConfig;

    [Inject] private ConfigsRepository _configRepository;

    private void Awake()
    {
        _levelConfig = GetLevelConfig();
        _spawnableObjectDataFactory = new SpawnableObjectDataFactory(_configRepository);
        _defendersData = _spawnableObjectDataFactory.GetDefendersData();
        _projectilesData = _spawnableObjectDataFactory.GetProjectilesData();
        _enemiesData = _spawnableObjectDataFactory.GetEnemiesData();
        _enemySpawnerHandler = new SpawnersHandler<Enemy>(_enemiesData);
        _enemySpawnHandler = new EnemySpawnHandler(_levelConfig, _enemySpawnerHandler, _spline, _enemiesData, _spawnDetector);

        _cellHandler = new CellHandler(_enemiesData, _enemySpawnHandler);
        _unitViewHandler = new UnitViewHandler(_cellHandler, _projectileButtonHandler);
        _projectileSpawnerHandler = new SpawnersHandler<Projectile>(_projectilesData);

        _defenderSpawnerHandler = new SpawnersHandler<Defender>(_defendersData);
        _buildConstructor = new DefenderBuilder(_inputHandler, _buildPreviewer);
        _defenderSpawnHandler = new DefenderSpawnHandler(_unitViewHandler, _buildConstructor, _defendersData, _projectileSpawnerHandler, _defenderSpawnerHandler);
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
