using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DefenderSpawnHandler
{
    private UnitViewHandler _unitViewHandler;
    private DefenderBuilder _builder;
    private List<DefenderData> _defendersData = new List<DefenderData>();
    private SpawnersHandler<Projectile> _projectileSpawnHandler;
    private SpawnersHandler<Defender> _spawnersHandler;
    private DefenderData _currentDefenderData;

    public DefenderSpawnHandler(UnitViewHandler unitViewHandler, DefenderBuilder builder, List<DefenderData> data, SpawnersHandler<Projectile> projectileSpawnerHandler, SpawnersHandler<Defender> spawnersHandler)
    {     
        _unitViewHandler = unitViewHandler;
        _unitViewHandler.ButtonClicked += SetPreview;
        _defendersData = data;
        _builder = builder;
        _projectileSpawnHandler = projectileSpawnerHandler;
        _spawnersHandler = spawnersHandler;
        _builder.BuildInstalled += InitDefender;
    }

    private void SetPreview(ProjectileButton button)
    {
        foreach (var item in _defendersData)
        {
            if (item.Type == button.Type)
            {
                _currentDefenderData = item;
                _builder.TryCreateNewObject(item);
            }
        }
    }

    private void InitDefender(UnitPlatform platform)
    {
        Vector3 spawnPosition = platform.transform.position;
        Defender defender = _spawnersHandler.Spawn(_currentDefenderData.Type, spawnPosition);
        defender.Attacked += SpawnProjectile;
        _currentDefenderData = null;
    }

    private void SpawnProjectile(Enemy enemy)
    {
        Projectile projectile = _projectileSpawnHandler.Spawn(enemy.Type, enemy.transform.position);
    }
}
