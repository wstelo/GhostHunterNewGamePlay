using System.Collections.Generic;
using System.Linq;

public class ProjectileCellHandler
{
    private UnitLineHandler UnitLineHandler;
    private List<SpawnableObjectData<Projectile>> _spawnableObjectData = new List<SpawnableObjectData<Projectile>>();

    public ProjectileCellHandler(UnitLineHandler lineHandler, List<SpawnableObjectData<Projectile>> spawnableObjectDatas)
    {
        UnitLineHandler = lineHandler;
        _spawnableObjectData = spawnableObjectDatas;
    }

    public List<ProjectileCell> GetRequiredProjectileCells()
    {
        List<ElementTypes> elementTypes = new List<ElementTypes>();
        List<ProjectileCell> projectileCells = new List<ProjectileCell>();
        List<int> counts = new List<int>();
        elementTypes = UnitLineHandler.GetCurrentLevelTypes();

        int repeatableElementsCount = GetRepeatableElements(elementTypes).Count;

        if (elementTypes.Count > 0)
        {
            ElementTypes firstElement = elementTypes.First();

            counts = GenerateProjectileCount(repeatableElementsCount);

            foreach (int count in counts)
            {
                foreach (var elementData in _spawnableObjectData)
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

    public ProjectileCell GetRandomProjectileCell()
    {
        List<ElementTypes> elementTypes = new List<ElementTypes>();
        elementTypes = UnitLineHandler.GetCurrentLevelTypes();
        System.Random random = new System.Random();

        int randomIndex = random.Next(0, elementTypes.Count);
        int randomCount = random.Next(1, GameStaticData.MaximumProjectileCellNumber + 1);

        foreach (var elementData in _spawnableObjectData)
        {
            if (elementData.Type == elementTypes[randomIndex])
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

    private List<ElementTypes> GetLevelElements(List<ElementTypes> elementTypes)                   ////////////////////////////////////////////
    {
        List<ElementTypes> levelElementTypes = new List<ElementTypes>();

        foreach (ElementTypes elementType in elementTypes)
        {
            if (levelElementTypes.Contains(elementType) == false)
            {
                levelElementTypes.Add(elementType);
            }
        }

        return levelElementTypes;
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
                return repeatableElements;
            }
        }

        return repeatableElements;
    }






}
