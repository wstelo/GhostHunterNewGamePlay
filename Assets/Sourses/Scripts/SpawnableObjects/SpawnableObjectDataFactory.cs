using System.Collections.Generic;
using System.Linq;

public class SpawnableObjectDataFactory
{
    private List<ElementConfig> _elementConfigs;
    private List<DefenderConfig> _defenderConfigs;
    private List<EnemyConfig> _enemyConfigs;
    private List<ProjectileConfig> _projectileConfigs;

    public SpawnableObjectDataFactory(ConfigsRepository configRepository)
    {
        _elementConfigs = configRepository.ConfigList;
        _defenderConfigs = configRepository.DefenderConfigs;
        _enemyConfigs = configRepository.EnemyConfigs;
        _projectileConfigs = configRepository.ProjectileConfigs;
    }

    public List<ProjectileData> GetProjectilesData()
    {
        List<ProjectileData> projectilesData = new List<ProjectileData>();

        foreach (var item in _projectileConfigs)
        {
            var unitData = new ProjectileData(item.Prefab, item.ProjectileType);
            projectilesData.Add(unitData);
        }

        return projectilesData;
    }

    public List<DefenderData> GetDefendersData()
    {
        List<DefenderData> unitsData = new List<DefenderData>();

        foreach (var defender in _defenderConfigs)
        {
            var unitData = new DefenderData(defender.DefenderType, defender.Prefab, defender.UnitPreviewPrefab, defender.HitEffect);
            unitsData.Add(unitData);
        }

        return unitsData.ToList();
    }

    public List<EnemyData> GetEnemiesData()
    {
        List<EnemyData> enemiesData = new List<EnemyData>();

        foreach (var enemy in _enemyConfigs)
        {
            var unitData = new EnemyData(enemy.EnemyType, enemy.Prefab);
            enemiesData.Add(unitData);
        }

        return enemiesData.ToList();
    }
}
