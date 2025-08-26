using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsRepository : MonoBehaviour
{
    [SerializeField] private List<ElementConfig> _elementConfigs = new List<ElementConfig>();
    [SerializeField] private List<DefenderConfig> _defenderConfigs = new List<DefenderConfig>();
    [SerializeField] private List<EnemyConfig> _enemyConfigs = new List<EnemyConfig>();

    public List<ElementConfig> ConfigList => _elementConfigs;
    public List<DefenderConfig > DefenderConfigs => _defenderConfigs;
    public List<EnemyConfig > EnemyConfigs => _enemyConfigs;
}
