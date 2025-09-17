using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraveTypeHandler : MonoBehaviour
{
    [SerializeField] private List<Grave> _graves;

    private LevelConfig _levelConfig;
    private List<EnemiesLevelConfig> _bossEnemy = new List<EnemiesLevelConfig>();
    private List<ElementTypes> _currentLevelSimpleEnemiesTypes = new List<ElementTypes>();

    private System.Random _random = new System.Random();

    public void Init(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        SetLevelParams(_levelConfig);
        SetCurrentGraves();
    }

    private void SetCurrentGraves()
    {
        int currentBossCount = 0;

        foreach (var grave in _graves)
        {
            if(grave.IsSettedElements == false)
            {
                if (currentBossCount < _bossEnemy.Count)
                {
                    grave.Init(_bossEnemy[currentBossCount].ElementTypes);
                    currentBossCount++;
                }
                else
                {
                    grave.Init(GetRandomElement(_currentLevelSimpleEnemiesTypes));
                }
            }
        }
    }

    private ElementTypes GetRandomElement(List<ElementTypes> currentLevelSimpleEnemiesTypes)
    {
        if (currentLevelSimpleEnemiesTypes.Count == 0)
            return default;

        int randomIndex = _random.Next(0, currentLevelSimpleEnemiesTypes.Count);

        return currentLevelSimpleEnemiesTypes[randomIndex];
    }

    private void SetLevelParams(LevelConfig levelConfig)
    {
        foreach (var enemy in levelConfig.EnemiesLevelConfigs)
        {
            if(enemy.IsMultiple)
            {
                _bossEnemy.Add(enemy);
            }
            else
            {
                ElementTypes currentElement = enemy.ElementTypes.First();

                if(_currentLevelSimpleEnemiesTypes.Contains(currentElement) == false)
                {
                    _currentLevelSimpleEnemiesTypes.Add(currentElement);
                }
            }
        }
    }
}
