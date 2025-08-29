using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler
{
    private SpawnableObjectFactory _spawnableObjectFactory;       ////////////           EntryPoint
    private Dictionary<EnemyTypes, SpawnableObjectSpawner<Enemy>> _enemySpawners = new Dictionary<EnemyTypes, SpawnableObjectSpawner<Enemy>>();
    private Dictionary<EnemyTypes, EnemyData> _enemiesData = new Dictionary<EnemyTypes, EnemyData>();
    private Dictionary<DefenderTypes, SpawnableObjectSpawner<Defender>> _defenderSpawners = new Dictionary<DefenderTypes, SpawnableObjectSpawner<Defender>>();
    private Dictionary<DefenderTypes, DefenderData> _defendersData = new Dictionary<DefenderTypes, DefenderData>();
    private Dictionary<ProjectileTypes, SpawnableObjectSpawner<Projectile>> _projectileSpawners = new Dictionary<ProjectileTypes, SpawnableObjectSpawner<Projectile>>();
    private Dictionary<ProjectileTypes, ProjectileData> _projectileData = new Dictionary<ProjectileTypes, ProjectileData>();

    private List<ElementConfig> _elementConfigs;

    public SpawnerHandler(List<EnemyData> enemiesData, DefenderData defenderData, List<ProjectileData> projectileData, List<ElementConfig> elementConfigs)
    {
        _spawnableObjectFactory = new SpawnableObjectFactory();
        _elementConfigs = elementConfigs;

        SetSpawners(enemiesData, defenderData, projectileData);
    }

    public Defender SpawnDefender(DefenderTypes requiredType, ElementTypes requiredElement, Vector3 position, int projectileCount)
    {
        return Spawn(
            _defenderSpawners,
            requiredType,
            position,
            defender => defender.Init(requiredElement, GetColorByElementType(requiredElement), this, projectileCount));
    }

    public Enemy SpawnEnemy(EnemyTypes requiredType, ElementTypes reqiredElement, Vector3 position)
    {
        return Spawn(
            _enemySpawners,
            requiredType,
            position,
            enemy => enemy.Init(reqiredElement, requiredType, GetColorByElementType(reqiredElement)));
    }

    public Projectile SpawnProjectile(ProjectileTypes requiredType, ElementTypes requiredElements, Vector3 position)
    {
        return Spawn(
            _projectileSpawners,
            requiredType,
            position,
            projectile => projectile.Init(requiredElements, GetColorByElementType(requiredElements)));
    }

    private TReturn Spawn<TKey, TReturn>(
    Dictionary<TKey, SpawnableObjectSpawner<TReturn>> spawners,
    TKey requiredTypes,
    Vector3 position,
    Action<TReturn> initCallback)
    where TReturn : MonoBehaviour, ISpawnableObject<TReturn>
    {
        if (spawners.TryGetValue(requiredTypes, out var spawner))
        {
            var spawnableObject = spawner.EnableObject(position);

            if (spawnableObject != null)
            {
                initCallback(spawnableObject);
            }

            return spawnableObject;
        }

        return null;
    }

    private void SetSpawners(List<EnemyData> enemiesData, DefenderData defendersData, List<ProjectileData> projectileData)
    {
        foreach (var item in enemiesData)
        {
            _enemySpawners.Add(item.EnemyType, new SpawnableObjectSpawner<Enemy>(_spawnableObjectFactory, item.Prefab));
            _enemiesData.Add(item.EnemyType, item);
        }

        _defenderSpawners.Add(defendersData.DefenderType, new SpawnableObjectSpawner<Defender>(_spawnableObjectFactory, defendersData.Prefab));
        _defendersData.Add(defendersData.DefenderType, defendersData);

        foreach (var item in projectileData)
        {
            foreach (var currentItem in defendersData.Prefab.ProjectilesTypes)
            {
                if (item.ProjectileType == currentItem)
                {
                    _projectileSpawners.Add(currentItem, new SpawnableObjectSpawner<Projectile>(_spawnableObjectFactory, item.Prefab));
                    _projectileData.Add(currentItem, item);
                }
            }
        }
    }

    private Color GetColorByElementType(ElementTypes elementType)
    {
        Color color = Color.white;

        foreach (var item in _elementConfigs)
        {
            if (item.Type == elementType)
            {
                color = item.Color;

                break;
            }
        }

        return color;
    }
}
