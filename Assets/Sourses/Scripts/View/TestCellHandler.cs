using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestCellHandler
{
    private EnemySpawnHandler _enemySpawnHandler;         ///////////////////
    private ElementColorizer _colorizer;
    private List<ElementConfig> _elementConfigs = new List<ElementConfig>();

    private List<Enemy> _enemies = new List<Enemy>();
    private List<ElementTypes> _levelTypes = new List<ElementTypes>();


    private Dictionary<int, EnemiesLevelConfig> _queueRepeatableEnemies = new Dictionary<int, EnemiesLevelConfig>();


    private int _projectileButtonCount = 0;
    private static System.Random _random = new System.Random();

    public TestCellHandler(LevelConfig levelConfig, List<ElementConfig> elementConfigs, EnemySpawnHandler spawnHandler, int projectileButtonCount, ElementColorizer colorizer)
    {
        _levelTypes = GetFirstThreeElementTypesOnLevel(levelConfig);
        _elementConfigs = elementConfigs;
        _enemySpawnHandler = spawnHandler;

        _enemySpawnHandler.Spawned += AddEnemy;
        _projectileButtonCount = projectileButtonCount;
        _colorizer = colorizer;

        _queueRepeatableEnemies = GroupConsecutiveEnemiesConfig(levelConfig.EnemiesLevelConfigs);
    }

    public List<ProjectileCell> GetInitialCells()
    {
        List<ProjectileCell> cells = new List<ProjectileCell>();

        return cells;
    }

    public List<ProjectileCell> GetCellsByCurrentEnemies()
    {
        List<ProjectileCell> cells = new List<ProjectileCell>();

        cells = GetCells(_enemies.ToList());

        return cells;
    }

    private List<ProjectileCell> GetCells(List<Enemy> enemies)
    {
        List<ProjectileCell> cells = new List<ProjectileCell>();
        int maxCellValue = GetMaxConsecutiveCount(enemies);

        Enemy enemy = enemies.FirstOrDefault(enemy => enemy is BossEnemy);

        if (enemy != null)
        {
            List<ElementTypes> _elements = enemy.ElementTypes;
            int currentIndex = 0;

            while (cells.Count < 3 && currentIndex < _elements.Count)
            {

                cells = GetRequiredCellByType(_elements[currentIndex], enemy.CurrentHealth);
                currentIndex++;
            }

            while (cells.Count < _projectileButtonCount)
            {
                cells.Add(GetSingleRandomCell(_levelTypes, GetMaxConsecutiveCount(enemies)));
            }
        }
        else
        {
            int repeatableEnemyCount = 0;

            List<Enemy> currentEnemies = new List<Enemy>();

            Enemy currentFirstEnemy = null;

            while (cells.Count < 3)
            {
                if (enemies.Count > 0)
                {
                    currentFirstEnemy = enemies.First();
                }
                else
                {
                    return null;
                }

                foreach (var currentEnemy in enemies)
                {
                    if (currentFirstEnemy.ElementTypes.ExactMatch(currentEnemy.ElementTypes))
                    {
                        currentEnemies.Add(currentEnemy);
                        repeatableEnemyCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (var currentEnemy in currentEnemies)
                {
                    enemies.Remove(currentEnemy);
                }

                if (repeatableEnemyCount > 0)
                {
                    List<ProjectileCell> requiredCells = GetRequiredCellByType(currentFirstEnemy.ElementTypes.First(), repeatableEnemyCount);

                    foreach (var requiredCell in requiredCells)
                    {
                        cells.Add(requiredCell);
                    }
                }
            }

            while (cells.Count < _projectileButtonCount)
            {
                cells.Add(GetSingleRandomCell(_levelTypes, maxCellValue));
            }
        }

        return cells;
    }

    private ProjectileCell GetSingleRandomCell(List<ElementTypes> currentElements, int maxCountOnLevel)
    {
        int minCount = 1;

        if (currentElements.Count == 0 || maxCountOnLevel <= 0)
        {
            return null;
        }

        ElementTypes type = currentElements[_random.Next(currentElements.Count)];
        int count = _random.Next(minCount, maxCountOnLevel);
        ProjectileCell cell = new ProjectileCell(type, count, _colorizer.GetColorByElementType(type));

        return cell;
    }

    private List<ProjectileCell> GetRequiredCellByType(ElementTypes currentElement, int requiredCount)
    {
        List<ProjectileCell> cells = new List<ProjectileCell>();

        if (requiredCount > 0)
        {
            List<int> cellsCount = GenerateProjectileCount(requiredCount);

            foreach (var cellCount in cellsCount)
            {
                cells.Add(new ProjectileCell(currentElement, cellCount, _colorizer.GetColorByElementType(currentElement)));
            }

            return cells;
        }

        return null;
    }



    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);

        foreach (var enemyElement in enemy.ElementTypes)
        {
            if (_levelTypes.Contains(enemyElement) == false)
            {
                _levelTypes.Add(enemyElement);
            }
        }

        enemy.Disabled += RemoveEnemy;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);

        foreach (var enemyElement in enemy.ElementTypes)
        {
            foreach (var currentEnemy in _enemies)
            {
                if (currentEnemy.ElementTypes.Contains(enemyElement) == false)
                {
                    _levelTypes.Remove(enemyElement);
                }
            }
        }

        enemy.Disabled -= RemoveEnemy;
    }

    private List<int> GenerateProjectileCount(int repeatableElementsCount)
    {
        List<int> result = new List<int>();
        int splitCount = TrySplit(repeatableElementsCount);

        switch (splitCount)
        {
            case 1:                                                                              /////////////////magic
                result.Add(repeatableElementsCount);
                break;

            case 2:
                result = GetSplittedCount(repeatableElementsCount, splitCount);
                break;

            case 3:
                result = GetSplittedCount(repeatableElementsCount, splitCount);
                break;
        }

        return result;
    }

    private List<int> GetSplittedCount(int repeatableElements, int splitCount)
    {
        int minCount = 1;
        List<int> result = new List<int>();
        System.Random random = new System.Random();
        int currentElements = repeatableElements;

        currentElements -= splitCount;

        for (int i = 0; i < splitCount; i++)
        {
            result.Add(minCount);
        }

        for (int i = 0; i < currentElements; i++)
        {
            int randomIndex = random.Next(result.Count);
            result[randomIndex] = result[randomIndex] + minCount;
        }

        return result;
    }

    private int TrySplit(int elementsCount)
    {
        const int CountToWithoutSplit = 1;
        const int MinCountToSingleSplit = 2;

        int currentCount = 0;
        int minChanceToSplit = 0;
        int maxChanceToSplit = 100;
        int countWithoutSplit = 1;
        int countToDoubleSplit = 2;
        int countToTripleSplit = 3;

        System.Random random = new System.Random();
        int randomValue = random.Next(minChanceToSplit, maxChanceToSplit + 1);

        switch (elementsCount)
        {
            case CountToWithoutSplit:
                currentCount = countWithoutSplit;
                break;

            case MinCountToSingleSplit:

                int minChanceToWithoutSplit = 50;

                if (randomValue <= minChanceToWithoutSplit)
                {
                    currentCount = countWithoutSplit;
                }
                else
                {
                    currentCount = countToDoubleSplit;
                }
                break;

            default:
                int maxChanceToWithoutSplit = 33;
                int maxChanceToTripleSplit = 66;

                if (randomValue <= maxChanceToWithoutSplit)
                {
                    currentCount = countWithoutSplit;
                }
                else if (randomValue > maxChanceToWithoutSplit && randomValue < maxChanceToTripleSplit)
                {
                    currentCount = countToDoubleSplit;
                }
                else if (randomValue >= maxChanceToTripleSplit)
                {
                    currentCount = countToTripleSplit;
                }
                break;
        }

        return currentCount;
    }

    private List<ElementTypes> GetFirstThreeElementTypesOnLevel(LevelConfig levelConfig)
    {
        List<ElementTypes> elements = new List<ElementTypes>();

        foreach (var enemyConfig in levelConfig.EnemiesLevelConfigs)
        {
            foreach (var element in enemyConfig.ElementTypes)
            {
                if (elements.Contains(element) == false)
                {
                    if (elements.Count > 2)                                                                            ////////////////////////////////////            MAGIC
                    {
                        break;
                    }

                    elements.Add(element);
                }
            }
        }

        return elements;
    }

    private int GetMaxConsecutiveCount(List<Enemy> enemies)
    {
        if (enemies == null || enemies.Count == 0)
        {
            return 0;
        }

        Enemy tempEnemy = enemies[0];
        int maxConsecutiveCount = 1;
        int currentCount = 1;

        foreach (var enemy in enemies)
        {
            if (tempEnemy.ElementTypes.ExactMatch(enemy.ElementTypes))
            {
                currentCount++;
                maxConsecutiveCount = Mathf.Max(maxConsecutiveCount, currentCount);
            }
            else
            {
                tempEnemy = enemy;
                currentCount = 1;
            }
        }

        return maxConsecutiveCount;
    }

    private Dictionary<int, EnemiesLevelConfig> GroupConsecutiveEnemiesConfig(List<EnemiesLevelConfig> enemies)
    {
        Dictionary<int, EnemiesLevelConfig> groupedEnemies = new Dictionary<int, EnemiesLevelConfig>();

        if (enemies == null || enemies.Count == 0)
        {
            return groupedEnemies;
        }

        int groupIndex = 1;
        List<ElementTypes> currentElements = enemies.First().ElementTypes;
        List<EnemiesLevelConfig> currentGroup = new List<EnemiesLevelConfig>();

        foreach (var enemy in enemies)
        {
            groupedEnemies.Add(groupIndex, enemy);
            groupIndex++;
        }

        return groupedEnemies;
    }
}
