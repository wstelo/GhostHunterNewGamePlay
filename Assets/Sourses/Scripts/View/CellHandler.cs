using System;
using System.Collections.Generic;
using System.Linq;

public class CellHandler
{
    private List<ElementConfig> _elementConfigs = new List<ElementConfig>();
    private List<ElementTypes> _levelTypes = new List<ElementTypes>();
    private EnemySpawnHandler _enemySpawnHandler;         ///////////////////

    private List<Enemy> _enemies = new List<Enemy>();
    private int _projectileButtonCount = 0;

    private static Random _random = new Random();

    public CellHandler(List<ElementTypes> elementTypes, List<ElementConfig> elementConfigs, EnemySpawnHandler spawnHandler, int projectileButtonCount)
    {
        _levelTypes = elementTypes;
        _elementConfigs = elementConfigs;
        _enemySpawnHandler = spawnHandler;

        _enemySpawnHandler.Spawned += AddEnemy;
        _projectileButtonCount = projectileButtonCount;
    }

    public List<ProjectileCell> GetRequiredProjectilesCellsTEST()
    {
        List<ProjectileCell> cells = new List<ProjectileCell>();

        Enemy enemy = _enemies.FirstOrDefault(enemy => enemy is BossEnemy);

        if (enemy != null)
        {
            cells = GetBossCells(enemy);

            if(cells.Count < _projectileButtonCount)
            {
                int requiredCount = _projectileButtonCount - cells.Count;

                for (int i = 0; i < requiredCount; i++)
                {
                    cells.Add(GetRandomProjectileCell());
                }
            }
        }
        else
        {

        }

        return cells;
    }

    private List<ProjectileCell> GetSimpleCells()
    {
        List<ProjectileCell> cells = new List<ProjectileCell>();

        Enemy firstEnemy = _enemies.First();

        foreach (var enemy in _enemies)
        {
            if(firstEnemy.ElementTypes.ExactMatch(enemy.ElementTypes))
            {
                if(firstEnemy.ElementTypes.Count == 1)
                {
                   // cells.Add(new ProjectileCell())
                }
                
            }
        }

        return null;
    }

    private List<ProjectileCell> GetBossCells(Enemy bossEnemy)
    {
        List<ProjectileCell> projectileCells = new List<ProjectileCell>();
        List<ElementTypes> requiredElements = bossEnemy.ElementTypes;
        ElementTypes elementTypes = bossEnemy.ElementTypes[_random.Next(bossEnemy.ElementTypes.Count)];

        int requiredCount = bossEnemy.CurrentHealth;
        List<int> cells = GenerateProjectileCount(requiredCount);

        foreach (var cell in cells)
        {
            foreach (var elementData in _elementConfigs)
            {
                if(elementData.Type == elementTypes)
                {
                    projectileCells.Add(new ProjectileCell(elementData.Type, cell, elementData.Color));
                }
            }
        }

        return projectileCells;
    }

    public List<ProjectileCell> GetRequiredProjectileCells()
    {
        List<ProjectileCell> projectileCells = new List<ProjectileCell>();
        List<int> counts = new List<int>();

        int repeatableElementsCount = GetRepeatableElements(_levelTypes).Count;

        if (_levelTypes.Count > 0)
        {
            ElementTypes firstElement = _levelTypes.First();

            counts = GenerateProjectileCount(repeatableElementsCount);

            foreach (int count in counts)
            {
                foreach (var elementData in _elementConfigs)
                {
                    if (elementData.Type == firstElement)
                    {
                        projectileCells.Add(new ProjectileCell(firstElement, count, elementData.Color));
                    }
                }
            }
        }
        else
        {
            return null;
        }

        return projectileCells;
    }

    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);

        enemy.Disabled += RemoveEnemy;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Disabled -= RemoveEnemy;
    }

    private List<ElementTypes> GetRepeatableElements(List<ElementTypes> elementTypes)
    {
        List<ElementTypes> repeatableElements = new List<ElementTypes>();

        foreach (ElementTypes elementType in elementTypes)
        {

            if (elementTypes.First() == elementType)
            {
                repeatableElements.Add(elementType);
            }
            else
            {
                break;
            }
        }

        return repeatableElements;
    }

    public ProjectileCell GetRandomProjectileCell()
    {
        System.Random random = new System.Random();

        int randomIndex = random.Next(0, _levelTypes.Count);
        int randomCount = random.Next(1, GameStaticData.MaximumProjectileCellNumber + 1);

        foreach (var elementData in _elementConfigs)
        {
            if (elementData.Type == _levelTypes[randomIndex])
            {
                ProjectileCell cell = new ProjectileCell(elementData.Type, randomCount, elementData.Color);

                return cell;
            }
        }

        return null;
    }

    private List<int> GenerateProjectileCount(int repeatableElementsCount)
    {
        List<int> result = new List<int>();
        int splitCount = TrySplit(repeatableElementsCount);

        switch (splitCount)
        {
            case 1:
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
}
