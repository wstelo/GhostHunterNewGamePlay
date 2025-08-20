using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterfaceDataFactory
{
    private List<ElementConfig> _configList;

    public InterfaceDataFactory(ConfigsRepository configRepository)
    {
        _configList = configRepository.ConfigList;
    }

    public List<InterfaceObjectData<Unit>> GetUnitsData()
    {
        List<InterfaceObjectData<Unit>> unitsData = new List<InterfaceObjectData<Unit>>();

        foreach (var item in _configList)
        {
          //  var unitData = new InterfaceObjectData<Unit>(item.Type, item.Prefab, item.Color);
           // unitsData.Add(unitData);
        }

        return unitsData.ToList();
    }

    public List<SpawnableObjectData<Projectile>> GetProjectileData()
    {
        List<SpawnableObjectData<Projectile>> projectilesData = new List<SpawnableObjectData<Projectile>>();

        foreach (var item in _configList)
        {
        //    var projectileData = new SpawnableObjectData<Projectile>(item.Type, item.ProjectilePrefab, item.Color);
     //       projectilesData.Add(projectileData);
        }

        return projectilesData.ToList();
    }
}
