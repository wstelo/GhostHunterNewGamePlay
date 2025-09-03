using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawnHandler
{
    private SpawnersHandler _spawnerHandler;
    private SplineContainer _splineContainer;
    private LevelConfig _levelConfig;
    private Vector3 _spawnPosition;
    private EnemySpawnPointDetector _enemySpawnDetector;

    private int _spawnedEnemyCount = 0;
    private int _currentEnemiesConfigIndex = 0;

    public EnemySpawnHandler(LevelConfig config, SpawnersHandler spawnerHandler, SplineContainer splineContainer, EnemySpawnPointDetector enemySpawnDetector)
    {
        _levelConfig = config;
        _spawnerHandler = spawnerHandler;
        _splineContainer = splineContainer;
        _spawnPosition = GetSpawnPoint(_splineContainer);
        _enemySpawnDetector = enemySpawnDetector;
        _enemySpawnDetector.Detected += CreateObject;
        _enemySpawnDetector.Destroyed += Unsubscribe;

        CreateObject();
    }

    private void CreateObject()
    {
        if (_currentEnemiesConfigIndex < _levelConfig.EnemiesLevelConfigs.Count)
        {
            EnemiesLevelConfig currentEnemy = _levelConfig.EnemiesLevelConfigs[_currentEnemiesConfigIndex];

            Enemy enemy = _spawnerHandler.SpawnEnemy(currentEnemy.EnemyType, currentEnemy.ElementTypes, _spawnPosition);

            if (enemy != null)
            {
                enemy.SetMover(_splineContainer, _levelConfig.LevelSpeed);
            }

            _spawnedEnemyCount++;

            if (_spawnedEnemyCount >= currentEnemy.Count)
            {
                _spawnedEnemyCount = 0;
                _currentEnemiesConfigIndex++;
            }
        }
    }

    private void Unsubscribe()
    {
        _enemySpawnDetector.Detected -= CreateObject;
        _enemySpawnDetector.Destroyed -= Unsubscribe;
    }

    private Vector3 GetSpawnPoint(SplineContainer splineContainer)
    {
        Vector3 point = _splineContainer.Splines.First().Knots.First().Position;

        return point;
    }

}
