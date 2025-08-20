using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Splines;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private ChargingPrjectileCellHandler _chargingPrjectileCellHandler;
    [SerializeField] private ProjectileButtonHandler _projectileButtonHandler;
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private TextAsset _jsonLevelConfig;
    [SerializeField] private UnitPreviewer _unitPreviewer;

    private List<SpawnableObjectData<Projectile>> _projectileData = new List<SpawnableObjectData<Projectile>>();
    private List<SpawnableObjectData<Ghost>> _unitsData = new List<SpawnableObjectData<Ghost>>();
    private SpawnableObjectDataFactory _spawnableObjectDataFactory;
    private SpawnerHandler<Ghost> _unitSpawnerHandler;
    private EnemySpawnHandler _enemySpawnHandler;
    private UnitViewHandler _unitViewHandler;
    private CellHandler _cellHandler;
    private UnitSpawnHandler _unitSpawnHandler;

    private LevelConfig _levelConfig;

    [Inject] private ConfigsRepository _configRepository;

    private void Awake()
    {
        // Init(GameData.Instance.CurrentLevelData);
        _levelConfig = GetLevelConfig();
         _spawnableObjectDataFactory = new SpawnableObjectDataFactory(_configRepository);
        _unitsData = _spawnableObjectDataFactory.GetUnitsData();
        _unitSpawnerHandler = new SpawnerHandler<Ghost>(_unitsData);
        _enemySpawnHandler = new EnemySpawnHandler(_levelConfig, _unitSpawnerHandler, _spline);
        _projectileData = _spawnableObjectDataFactory.GetProjectileData();
        _cellHandler = new CellHandler(_projectileData, _enemySpawnHandler);
        _unitViewHandler = new UnitViewHandler(_cellHandler, _projectileButtonHandler);
        //
        _unitSpawnHandler = new UnitSpawnHandler(_unitViewHandler, _unitPreviewer, _unitsData);
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
