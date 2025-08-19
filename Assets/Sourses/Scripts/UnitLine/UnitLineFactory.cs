using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLineFactory 
{
    private SpawnerHandler<Ghost> _spawner;
    private UnitLine _prefab;
    private float _lineSpeed;

    public UnitLineFactory(SpawnerHandler<Ghost> spawner, UnitLine prefab, float lineSpeed)
    {
        _spawner = spawner;
        _prefab = prefab;
        _lineSpeed = lineSpeed;
    }

    public UnitLine GetLine(List<ElementTypes> elements)
    {
        UnitLine line = Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);

        List<Ghost> ghosts = new List<Ghost>();

        for (int i = 0; i < elements.Count; i++)
        {
            ghosts.Add(_spawner.Spawn(elements[i], line.PointCollector.TargetPoints[i].transform.position));
        }

        foreach (Ghost ghost in ghosts)
        {
            ghost.transform.SetParent(line.transform, false);
        }

        line.Init(ghosts, _lineSpeed);
        line.transform.position = GameStaticData.UnitLineSpawnPoint;

        return line;
    }
}
