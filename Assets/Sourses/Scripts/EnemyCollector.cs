using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollector
{
    private EnemySpawnHandler _spawnHandler;

    private List<Enemy> _enemies = new List<Enemy>();

    public EnemyCollector(EnemySpawnHandler spawnHandler)
    {
        _spawnHandler = spawnHandler;

        _spawnHandler.Spawned += AddEnemy;
    }

    public Enemy TryGetTarget(Enemy enemy)
    {
        if (_enemies.Contains(enemy))
        {
            if (enemy.IsLastHealth)
            {
                _enemies.Remove(enemy);
            }

            return enemy;
        }

        return null;
    }

    public void ReturnTarget(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }
}
