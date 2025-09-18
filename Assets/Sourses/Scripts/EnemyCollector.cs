using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollector
{
    private EnemySpawnHandler _spawnHandler;

    private List<Enemy> _enemies;

    public EnemyCollector(EnemySpawnHandler spawnHandler)
    {
        _spawnHandler = spawnHandler;

        _spawnHandler.Spawned += AddEnemy;
    }

    public Enemy TryGetTarget(Enemy enemy)
    {
        if (_enemies.Contains(enemy))
        {
            return enemy;
        }

        return null;
    }

    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }
}
