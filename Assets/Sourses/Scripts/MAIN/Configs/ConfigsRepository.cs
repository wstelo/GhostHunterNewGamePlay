using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfigsRepository : MonoBehaviour
{
    [SerializeField] private List<ElementConfig> _elementConfigs = new List<ElementConfig>();
    [SerializeField] private List<DefenderConfig> _defenderConfigs = new List<DefenderConfig>();
    [SerializeField] private List<EnemyConfig> _enemyConfigs = new List<EnemyConfig>();
    [SerializeField] private List<ProjectileConfig> _projectileConfigs = new List<ProjectileConfig>();

    public List<ElementConfig> ElementConfigs => _elementConfigs.ToList();
    public List<DefenderConfig > DefenderConfigs => _defenderConfigs.ToList();
    public List<EnemyConfig > EnemyConfigs => _enemyConfigs.ToList();
    public List<ProjectileConfig > ProjectileConfigs => _projectileConfigs.ToList();
}