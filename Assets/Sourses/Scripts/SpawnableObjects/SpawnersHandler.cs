using System;
using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using UnityEngine;

public class SpawnersHandler
{
    private SpawnableObjectFactory _spawnableObjectFactory;
    private Dictionary<EnemyTypes, SpawnableObjectSpawner<Enemy>> _enemySpawners = new Dictionary<EnemyTypes, SpawnableObjectSpawner<Enemy>>();
    private Dictionary<DefenderTypes, SpawnableObjectSpawner<Defender>> _defenderSpawners = new Dictionary<DefenderTypes, SpawnableObjectSpawner<Defender>>();
    private Dictionary<ProjectileTypes, SpawnableObjectSpawner<Projectile>> _projectileSpawners = new Dictionary<ProjectileTypes, SpawnableObjectSpawner<Projectile>>();

    private Dictionary<EnemyTypes, EnemyConfig> _enemiesData = new Dictionary<EnemyTypes, EnemyConfig>();
    private Dictionary<DefenderTypes, DefenderConfig> _defendersData = new Dictionary<DefenderTypes, DefenderConfig>();
    private Dictionary<ProjectileTypes, ProjectileConfig> _projectileData = new Dictionary<ProjectileTypes, ProjectileConfig>();

    private List<ElementConfig> _elementConfigs = new List<ElementConfig>();

    public SpawnersHandler()
    {
        _spawnableObjectFactory = new SpawnableObjectFactory();
    }

    public void Init(ConfigsRepository configRepository)
    {
        _elementConfigs = configRepository.ConfigList;

        SetParameters(configRepository.EnemyConfigs, configRepository.DefenderConfigs.First(), configRepository.ProjectileConfigs);
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

    public Defender SpawnDefender(DefenderTypes requiredType, List<ElementTypes> requiredElement, Vector3 position, int projectileCount)
    {
        _defendersData.TryGetValue(requiredType, out DefenderConfig data);

        return Spawn(
            _defenderSpawners,
            requiredType,
            position,
            defender => defender.Init(requiredElement, GetMultipleElementColor(requiredElement), this, projectileCount, data));
    }

    public Enemy SpawnEnemy(EnemyTypes requiredType, List<ElementTypes> reqiredElements, Vector3 position)
    {
        return Spawn(
            _enemySpawners,
            requiredType,
            position,
            enemy => enemy.Init(reqiredElements, requiredType, GetMultipleElementColor(reqiredElements)));
    }

    public Projectile SpawnProjectile(ProjectileTypes requiredType, ElementTypes requiredElements, Vector3 position)
    {
        return Spawn(
            _projectileSpawners,
            requiredType,
            position,
            projectile => projectile.Init(requiredElements, GetColorByElementType(requiredElements)));
    }

    private void SetParameters(List<EnemyConfig> enemiesData, DefenderConfig defendersData, List<ProjectileConfig> projectileData)
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

    private List<Color> GetMultipleElementColor (List<ElementTypes> elementTypes)          //////////////////////////////////
    {
        List<Color> color = new List<Color>();

        if(elementTypes.Count > 1)
        {
            foreach (var item in _elementConfigs)
            {
                foreach(var type in elementTypes)
                {
                    if (item.Type == type)
                    {
                        color.Add(item.Color);
                    }
                }
            }
        }
        else
        {
            foreach (var item in _elementConfigs)
            {
                if (item.Type == elementTypes.First())
                {
                    color.Add(item.Color);

                    break;
                }
            }
        }

        return color;
    }

    private Color GetColorByElementType(ElementTypes elementType)                          /////////////////////////////////////
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
