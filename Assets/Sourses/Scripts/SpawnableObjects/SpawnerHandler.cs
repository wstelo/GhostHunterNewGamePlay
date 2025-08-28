using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler
{
    private SpawnableObjectFactory _spawnableObjectFactory;       //////////// вынести в EntryPoint
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

    //public TReturn Spawn <TKey, TReturn>(Dictionary<TKey, object> data, TKey requiredTypes, ElementTypes reqiredElements, Vector3 position, int projectileCount)            /////////////////////////// дубляж 
    //{
    //    TReturn spawnableObject;

    //    if (data.TryGetValue(requiredTypes, out var spawner))
    //    {         
    //        spawnableObject = spawner.EnableObject(position);

    //        if (_defendersData.TryGetValue(data, out var data))
    //        {
    //            spawnableObject.Init(reqiredElements, GetColorByElementType(reqiredElements), this, projectileCount);                 ////////////////////////////// передаём спавнер юниту
    //        }

    //        return spawnableObject;
    //    }

    //    return default(TReturn);
    //}

    public Defender SpawnDefender(DefenderTypes requiredTypes, ElementTypes reqiredElements, Vector3 position, int projectileCount)            /////////////////////////// дубляж 
    {
        Defender spawnableObject;

        if (_defenderSpawners.TryGetValue(requiredTypes, out var spawner))
        {
            var currentSpawner = spawner;
            spawnableObject = currentSpawner.EnableObject(position);

            if (_defendersData.TryGetValue(requiredTypes, out var data))
            {
                spawnableObject.Init(reqiredElements, GetColorByElementType(reqiredElements), this, projectileCount);                 ////////////////////////////// передаём спавнер юниту
            }

            return spawnableObject;
        }

        return null;
    }

    public Enemy SpawnEnemy(EnemyTypes requiredTypes, ElementTypes reqiredElements, Vector3 position)
    {
        Enemy spawnableObject;

        if (_enemySpawners.TryGetValue(requiredTypes, out var spawner))
        {
            var currentSpawner = spawner;
            spawnableObject = currentSpawner.EnableObject(position);

            if (_enemiesData.TryGetValue(requiredTypes, out var data))
            {
                spawnableObject.Init(reqiredElements, data.EnemyType, GetColorByElementType(reqiredElements));
            }

            return spawnableObject;
        }

        return null;
    }

    public Projectile SpawnProjectile(ProjectileTypes requiredType,  ElementTypes reqiredElements, Vector3 position)
    {
        Projectile spawnableObject;

        if (_projectileSpawners.TryGetValue(requiredType, out var spawner))
        {
            var currentSpawner = spawner;
            spawnableObject = currentSpawner.EnableObject(position);

            if (_projectileData.TryGetValue(requiredType, out var data))
            {
                spawnableObject.Init(reqiredElements, GetColorByElementType(reqiredElements));
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
            foreach(var currentItem in defendersData.Prefab.ProjectilesTypes)
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
