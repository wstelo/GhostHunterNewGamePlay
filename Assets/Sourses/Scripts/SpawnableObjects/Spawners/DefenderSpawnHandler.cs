using UnityEngine;

public class DefenderSpawnHandler
{
    private SpawnersHandler _spawnersHandler;
    private DefenderConfig _config;                               ///////////////////////////////////////////                     Õ”∆≈Õ  ŒÕ‘»√

    public DefenderSpawnHandler(SpawnersHandler spawnHandler, DefenderConfig currentConfig)
    {
        _spawnersHandler = spawnHandler;
        _config = currentConfig;
    }

    public Defender SpawnDefender(MultiProjectileCell cell, Vector3 position)
    {
        Defender defender = _spawnersHandler.SpawnDefender(DefenderTypes.Magician, cell.ElementTypes, position, cell.Count);                 /////////////////////////////////////////////////////////  DEFENDERTYPE

        return defender;
    }
}
