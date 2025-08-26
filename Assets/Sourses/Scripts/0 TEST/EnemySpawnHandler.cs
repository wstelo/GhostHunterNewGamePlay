using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawnHandler
{
    private List<EnemyData> _enemiesData;
    private SpawnersHandler<Enemy> _spawnerHandler;
    private List<ElementTypes> _elementTypes;
    private SplineContainer _splineContainer;
    private LevelConfig _levelConfig;
    private Vector3 _spawnPosition;
    private EnemySpawnPointDetector _enemySpawnDetector;

    public EnemySpawnHandler(LevelConfig config, SpawnersHandler<Enemy> spawnerHandler, SplineContainer splineContainer, List<EnemyData> _data, EnemySpawnPointDetector enemySpawnDetector)
    {
        _levelConfig = config;
        _spawnerHandler = spawnerHandler;
   //     _elementTypes = GetCurrentTypes(config.ButtonValues);
        _splineContainer = splineContainer;
        _spawnPosition = GetSpawnPoint(_splineContainer);
        _enemiesData = _data;
        _enemySpawnDetector = enemySpawnDetector;
        _enemySpawnDetector.Detected += CreateObject;
        _enemySpawnDetector.Destroyed += Unsubscribe;

        CreateObject();
    }

    private void Unsubscribe()
    {
        _enemySpawnDetector.Detected -= CreateObject;
        _enemySpawnDetector.Destroyed -= Unsubscribe;
    }

    public List<ElementTypes> GetCurrentLevelTypes()
    {
        return _elementTypes;
    }

    private Vector3 GetSpawnPoint(SplineContainer splineContainer)
    {
        Vector3 point = _splineContainer.Splines.First().Knots.First().Position;

        return point;
    }

    private List<ElementTypes> GetCurrentTypes(ElementTypes[,] ghostColorList)
    {
        List<ElementTypes> elements = new List<ElementTypes>();

        for (int i = 0; i < ghostColorList.GetLength(0); i++)
        {
            for (int j = 0; j < ghostColorList.GetLength(1); j++)
            {
                elements.Add(ghostColorList[i, j]);
            }
        }

        return elements;
    }

    private void CreateObject()
    {
        if (_elementTypes.Count > 0)
        {
            ElementTypes firstElement = _elementTypes.First();
            Enemy enemy = _spawnerHandler.Spawn(firstElement, _spawnPosition);                               //////////////////////////////////////////

            foreach (var data in _enemiesData)
            {
                if (firstElement == data.Type)
                {
                    enemy.SetMover(_splineContainer, _levelConfig.LevelSpeed);
                    _elementTypes.RemoveAt(0);                                                                 /////////////////////////////////////////                РАсчёт кнопок не корректный из-за репитабл каунта из-за удаления
                }
            }
        }
    }
}
