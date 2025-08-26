using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnableObjectDataFactory
{
    private List<ElementConfig> _elementConfigs;
    private List<DefenderConfig> _defenderConfigs;
    private List<EnemyConfig> _enemyConfigs;

    public SpawnableObjectDataFactory(ConfigsRepository configRepository)
    {
        _elementConfigs = configRepository.ConfigList;
        _defenderConfigs = configRepository.DefenderConfigs;
        _enemyConfigs = configRepository.EnemyConfigs;
    }

    public List<ProjectileData> GetProjectilesData()
    {
        List<ProjectileData> projectilesData = new List<ProjectileData>();

        foreach (var defender in _defenderConfigs)
        {
            foreach (var element in _elementConfigs)
            {
                var unitData = new ProjectileData(element.Type, element.Color, defender.ProjectilePrefab);
                projectilesData.Add(unitData);
            }
        }

        return projectilesData;
    }

    public List<DefenderData> GetDefendersData()
    {
        List<DefenderData> unitsData = new List<DefenderData>();

        foreach (var defender in _defenderConfigs)
        {
            foreach (var element in _elementConfigs)
            {
                var unitData = new DefenderData(element.Type, defender.Prefab, element.Color, defender.UnitPreviewPrefab, defender.ProjectilePrefab, defender.HitEffect);
                unitsData.Add(unitData);
            }
        }

        
        return unitsData.ToList();
    }

    public List<EnemyData> GetEnemiesData()
    {
        List<EnemyData> enemiesData = new List<EnemyData>();

        foreach (var enemy in _enemyConfigs)
        {
            foreach (var element in _elementConfigs)
            {
                var unitData = new EnemyData(element.Type, enemy.UnitPrefab, element.Color);
                enemiesData.Add(unitData);
            }
        }

        return enemiesData.ToList();
    }
}
