using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : SpawnableObjectData<Projectile>
{
    public ProjectileData(ElementTypes type, Color typeColor, Projectile prefab)
    {
        Inittialize(type, typeColor, prefab);
    }
}
