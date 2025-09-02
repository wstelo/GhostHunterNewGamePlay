using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersHandler
{
    private SpawnableObjectFactory _spawnableObjectFactory;
    private Dictionary<EnemyTypes, SpawnableObjectSpawner<Enemy>> _enemySpawners = new Dictionary<EnemyTypes, SpawnableObjectSpawner<Enemy>>();
    private Dictionary<DefenderTypes, SpawnableObjectSpawner<Defender>> _defenderSpawners = new Dictionary<DefenderTypes, SpawnableObjectSpawner<Defender>>();
    private Dictionary<ProjectileTypes, SpawnableObjectSpawner<Projectile>> _projectileSpawners = new Dictionary<ProjectileTypes, SpawnableObjectSpawner<Projectile>>();

    private Dictionary<EnemyTypes, EnemyData> _enemiesData = new Dictionary<EnemyTypes, EnemyData>();
    private Dictionary<DefenderTypes, DefenderData> _defendersData = new Dictionary<DefenderTypes, DefenderData>();
    private Dictionary<ProjectileTypes, ProjectileData> _projectileData = new Dictionary<ProjectileTypes, ProjectileData>();

    private List<ElementConfig> _elementConfigs;

    public SpawnersHandler(List<EnemyData> enemiesData, DefenderData defenderData, List<ProjectileData> projectileData, List<ElementConfig> elementConfigs)
    {
        _spawnableObjectFactory = new SpawnableObjectFactory();
        _elementConfigs = elementConfigs;

        SetParameters(enemiesData, defenderData, projectileData);
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

    public Defender SpawnDefender(DefenderTypes requiredType, ElementTypes requiredElement, Vector3 position, int projectileCount)
    {
        _defendersData.TryGetValue(requiredType, out DefenderData data);

        return Spawn(
            _defenderSpawners,
            requiredType,
            position,
            defender => defender.Init(requiredElement, GetColorByElementType(requiredElement), this, projectileCount, data));
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

    private void SetParameters(List<EnemyData> enemiesData, DefenderData defendersData, List<ProjectileData> projectileData)
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
            if (item.ProjectileType == defendersData.Prefab.ProjectileType)
            {
                _projectileSpawners.Add(item.ProjectileType, new SpawnableObjectSpawner<Projectile>(_spawnableObjectFactory, item.Prefab));
                _projectileData.Add(item.ProjectileType, item);
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
