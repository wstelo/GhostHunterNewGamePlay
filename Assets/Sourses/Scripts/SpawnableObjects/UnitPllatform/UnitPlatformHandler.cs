using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlatformHandler : MonoBehaviour
{
    [SerializeField] private List<UnitPlatform> _platforms;

    public void Initialize(DefenderSpawnHandler spawnHandler)
    {
        foreach (UnitPlatform platform in _platforms)
        {
            platform.Initialize(spawnHandler);
        }
    }
}
