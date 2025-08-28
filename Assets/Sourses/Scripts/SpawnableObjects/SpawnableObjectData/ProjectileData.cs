using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : SpawnableObjectData<Projectile>
{
    public ProjectileTypes ProjectileType;

    public ProjectileData(Projectile prefab, ProjectileTypes projectileType)  //////// лишние поля
    {
        ProjectileType = projectileType;
        Inittialize(prefab);
    }
}
