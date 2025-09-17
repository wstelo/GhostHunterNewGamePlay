using System;
using System.Collections.Generic;
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

    public EnemySpawnHandler(LevelConfig config, SpawnersHandler spawnerHandler, SplineContainer splineContainer, float enemyDistance, float moveSpeed)
    {
        _levelConfig = config;
        _spawnerHandler = spawnerHandler;
        _splineContainer = splineContainer;
        _spawnPosition = GetSpawnPoint(_splineContainer);

        _spawnDelay = enemyDistance / moveSpeed;

        Spawn().Forget();
    }

    private async UniTaskVoid Spawn()
    {
        try
        {
            while (_currentEnemiesConfigIndex < _levelConfig.EnemiesLevelConfigs.Count)
            {
                CreateObject();
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnDelay));
            }
        }
        catch
        {
            throw new Exception("EnemySpawn");
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
        }
    }

    private Vector3 GetSpawnPoint(SplineContainer splineContainer)
    {
        Vector3 point = _splineContainer.Splines.First().Knots.First().Position;

        return point;
    }

}
