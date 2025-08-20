using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Splines;

public class InterfaceEntryPoint : MonoBehaviour
{
    [SerializeField] private ChargingPrjectileCellHandler _chargingPrjectileCellHandler;
    [SerializeField] private ProjectileButtonHandler _projectileButtonHandler;
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private TextAsset _jsonLevelConfig;

    private List<InterfaceObjectData<Unit>> _unitsData = new List<InterfaceObjectData<Unit>>();
    private InterfaceDataFactory _spawnableObjectDataFactory;
    private InterfaceSpawnHandler<Unit> _unitSpawnerHandler;
    private EnemySpawnHandler _enemySpawnHandler;

    private LevelConfig _levelConfig;

    [Inject] private ConfigsRepository _configRepository;

    private void Awake()
    {
        // Init(GameData.Instance.CurrentLevelData);
        _levelConfig = GetLevelConfig();
        _spawnableObjectDataFactory = new InterfaceDataFactory(_configRepository);
        _unitsData = _spawnableObjectDataFactory.GetUnitsData();
        _unitSpawnerHandler = new InterfaceSpawnHandler<Unit>(_unitsData);
      //  _enemySpawnHandler = new EnemySpawnHandler(_levelConfig, _unitSpawnerHandler, _spline);
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
