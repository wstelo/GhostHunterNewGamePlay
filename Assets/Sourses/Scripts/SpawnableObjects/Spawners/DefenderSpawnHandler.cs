using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawnHandler
{
    private List<ElementTypes> _levelElementTypes = new List<ElementTypes>();
    private UnitViewHandler _unitViewHandler;
    private DefenderBuilder _builder;
    // private SpawnersHandler<Projectile> _projectileSpawnHandler;
    private SpawnerHandler _spawnersHandler;
    private DefenderData _currentDefenderData;
    private ElementTypes _currentDefenderType;
    private ProjectileButton _currentProjectileButton;

    public DefenderSpawnHandler(UnitViewHandler unitViewHandler, DefenderBuilder builder,SpawnerHandler spawnersHandler, DefenderData currentDefenderData)
    {
        _unitViewHandler = unitViewHandler;
        _unitViewHandler.ButtonClicked += SetPreview;
        _builder = builder;
        _currentDefenderData = currentDefenderData;        
        _spawnersHandler = spawnersHandler;
        _builder.BuildInstalled += InitDefender;
    }

    private void SetPreview(ProjectileButton button)
    {
        _currentProjectileButton = button;
        _currentDefenderType = button.Type;
        _builder.TryCreateNewObject(_currentDefenderData, button.Color);
    }

    private void InitDefender(UnitPlatform platform)
    {
        Vector3 spawnPosition = platform.transform.position;
        Defender defender = _spawnersHandler.SpawnDefender(_currentDefenderData.DefenderType, _currentDefenderType, spawnPosition, _currentProjectileButton.Count);
        defender.Attacked += SpawnProjectile;
        _currentProjectileButton = null;
    }

    private void SpawnProjectile(Enemy enemy)
    {
        // Projectile projectile = _projectileSpawnHandler.Spawn(enemy.ElementType, enemy.transform.position);
    }
}
