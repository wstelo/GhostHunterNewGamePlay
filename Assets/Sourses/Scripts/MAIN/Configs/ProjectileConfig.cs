using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileConfig", menuName = "NewProjectileConfig / NewConfig")]
public class ProjectileConfig : ScriptableObject
{
    [SerializeField] private ProjectileTypes _projectileType;
    [SerializeField] private Projectile _prefab;

    public ProjectileTypes ProjectileType => _projectileType;
    public Projectile Prefab => _prefab;
}
