using System.Collections.Generic;
using System.Linq;

public class SpawnableObjectDataFactory
{
    private List<ElementConfig> _configList;

    public SpawnableObjectDataFactory(ConfigsRepository configRepository)
    {
        _configList = configRepository.ConfigList;
    }

    public List<SpawnableObjectData<Ghost>> GetUnitsData()
    {
        List<SpawnableObjectData<Ghost>> unitsData = new List<SpawnableObjectData<Ghost>>();

        foreach (var item in _configList)
        {
            var unitData = new SpawnableObjectData<Ghost>(item.Type, item.UnitPrefab, item.Color, item.UnitPreviewPrefab);
            unitsData.Add(unitData);
        }

        return unitsData.ToList();
    }

    public List<SpawnableObjectData<Projectile>> GetProjectileData()
    {
        List<SpawnableObjectData<Projectile>> projectilesData = new List<SpawnableObjectData<Projectile>>();

        foreach (var item in _configList)
        {
            var projectileData = new SpawnableObjectData<Projectile>(item.Type, item.ProjectilePrefab, item.Color, item.UnitPreviewPrefab);
            projectilesData.Add(projectileData);               
        }

        return projectilesData.ToList();
    }
}
