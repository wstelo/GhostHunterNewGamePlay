using System.Collections.Generic;
using Newtonsoft.Json;
using Reflex.Attributes;
using UnityEngine;

public class CurrentLevelEntryPoint : MonoBehaviour
{
    [SerializeField] private ChargingPrjectileCellHandler _chargingPrjectileCellHandler;
    [SerializeField] private ProjectileButtonHandler _projectileButtonHandler;
    // [SerializeField] private ConfigsRepository _configRepository;
    [SerializeField] private UnitLine _unitLinePrefab;
    [SerializeField] private ProjectileHandler _projectileHandler;

    private List<SpawnableObjectData<Projectile>> _projectileData = new List<SpawnableObjectData<Projectile>>();
    private List<SpawnableObjectData<Ghost>> _unitsData = new List<SpawnableObjectData<Ghost>>();
    private SpawnableObjectDataFactory _spawnableObjectDataFactory;
    private SpawnerHandler<Ghost> _unitSpawnerHandler;
    private SpawnerHandler<Projectile> _projectileSpawnerHandler;
    private UnitLineHandler _unitLineHandler;
    private ProjectileViewHandler _projectileViewHandler;
    private ProjectileCellHandler _projectileCellHandler;
    private LevelConfig _levelConfig;

    [Inject] private ConfigsRepository _configRepository;

    private void Awake()
    {
         Init(GameData.Instance.CurrentLevelData);
        _spawnableObjectDataFactory = new SpawnableObjectDataFactory(_configRepository);
        _unitsData = _spawnableObjectDataFactory.GetUnitsData();
        _projectileData = _spawnableObjectDataFactory.GetProjectileData();
        _unitSpawnerHandler = new SpawnerHandler<Ghost>(_unitsData);
        _projectileSpawnerHandler = new SpawnerHandler<Projectile>(_projectileData);
        _unitLineHandler = new UnitLineHandler(_levelConfig, _unitSpawnerHandler, _unitLinePrefab);
        _projectileCellHandler = new ProjectileCellHandler(_unitLineHandler, _projectileData);
        _projectileViewHandler = new ProjectileViewHandler(_projectileCellHandler, _projectileButtonHandler, _chargingPrjectileCellHandler);
        _projectileHandler.Init(_projectileViewHandler, _projectileSpawnerHandler, _unitLineHandler);
    }

    public void Init(LevelData currentLevel)
    {
        _levelConfig = currentLevel.Config;
    }

    //private LevelConfig GetLevelConfig()
    //{
    //    string json = _jsonLevelConfig.text;
    //    LevelConfig levelConfig = JsonConvert.DeserializeObject<LevelConfig>(json);

    //    return levelConfig ?? throw new System.Exception("Failed deserialization from JSON");
    //}
}
