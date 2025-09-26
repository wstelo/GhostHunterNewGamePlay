using System;
using System.Drawing;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawnHandler
{
    private SpawnersHandler _spawnerHandler;
    private SplineContainer _splineContainer;
    private LevelConfig _levelConfig;
    private Vector3 _spawnPosition;
    private float _spawnDelay = 1f;

    private int _spawnedEnemyCount = 0;
    private int _currentEnemiesConfigIndex = 0;

    public EnemySpawnHandler(LevelConfig config, SpawnersHandler spawnerHandler, SplineContainer splineContainer, float distanceBetweenEnemy)
    {
        _levelConfig = config;
        _spawnerHandler = spawnerHandler;
        _splineContainer = splineContainer;
        _spawnPosition = _splineContainer.Splines.First().Knots.First().Position; ;
        _spawnDelay = distanceBetweenEnemy / config.LevelSpeed;

        Spawn().Forget();
    }

    public event Action <Enemy> Spawned;        /////////////////////

    private async UniTaskVoid Spawn()
    {
        while (_currentEnemiesConfigIndex < _levelConfig.EnemiesLevelConfigs.Count)             /////////////////////////////////// TOKEN
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_spawnDelay));
            CreateObject();         
        }
    }

    private void CreateObject()
    {
        if (_currentEnemiesConfigIndex < _levelConfig.EnemiesLevelConfigs.Count)
        {
            EnemiesLevelConfig currentEnemy = _levelConfig.EnemiesLevelConfigs[_currentEnemiesConfigIndex];

            Enemy enemy = _spawnerHandler.SpawnEnemy(currentEnemy.EnemyType, currentEnemy.ElementTypes, _spawnPosition, currentEnemy.Health, _splineContainer, _levelConfig.LevelSpeed);

            _spawnedEnemyCount++;

            if (_spawnedEnemyCount >= currentEnemy.Count)
            {
                _spawnedEnemyCount = 0;
                _currentEnemiesConfigIndex++;
            }

            Spawned?.Invoke(enemy);
        }
    }



}
