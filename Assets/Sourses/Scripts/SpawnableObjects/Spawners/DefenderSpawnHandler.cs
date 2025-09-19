using UnityEngine;

public class DefenderSpawnHandler
{
    private SpawnersHandler _spawnersHandler;
    private EnemyCollector _enemyCollector;
    private DefenderConfig _config;                               ///////////////////////////////////////////                     Õ”∆≈Õ  ŒÕ‘»√

    public DefenderSpawnHandler(SpawnersHandler spawnHandler, DefenderConfig currentConfig, EnemyCollector enemyCollector)
    {
        _spawnersHandler = spawnHandler;
        _config = currentConfig;
        _enemyCollector = enemyCollector;
    }

    public Defender SpawnDefender(MultiProjectileCell cell, Vector3 position)
    {
        Defender defender = _spawnersHandler.SpawnDefender(DefenderTypes.Magician, cell.ElementTypes, position, cell.Count);                 /////////////////////////////////////////////////////////  DEFENDERTYPE
        defender.SetEnemyCollector(_enemyCollector);

        return defender;
    }
}
