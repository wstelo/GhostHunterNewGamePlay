using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawnHandler
{
    private List<ElementTypes> _elementTypes;
    private SpawnerHandler<Ghost> _spawnerHandler;
    private SplineContainer _splineContainer;
    private Vector3 _spawnPosition;
    private LevelConfig _levelConfig;

    public EnemySpawnHandler(LevelConfig config, SpawnerHandler<Ghost> spawnerHandler, SplineContainer splineContainer)
    {
        _levelConfig = config;
        _spawnerHandler = spawnerHandler;
        _elementTypes = GetCurrentTypes(config.ButtonValues);
        _splineContainer = splineContainer;
        _spawnPosition = GetSpawnPoint(_splineContainer);
       
        CreateObject();
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
        if(_elementTypes.Count > 0)
        {
            Ghost ghost = _spawnerHandler.Spawn(_elementTypes.First(), _spawnPosition);
            ghost.InitMover(_splineContainer, _levelConfig.LevelSpeed);
            ghost.SpawnLineWalked += SpawnNewObject;
            _elementTypes.RemoveAt(0);
        }
    }

    private void SpawnNewObject(Ghost ghost)
    {
        CreateObject();
        ghost.SpawnLineWalked -= SpawnNewObject;
    }
}
