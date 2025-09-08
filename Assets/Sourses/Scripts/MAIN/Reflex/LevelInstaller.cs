using System.Collections;
using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;

public class LevelInstaller : MonoBehaviour, IInstaller
{
    private SpawnersHandler _unitSpawnerHandler;

    private void Awake()
    {
        _unitSpawnerHandler = new SpawnersHandler(_enemiesData, GetCurrentLevelDefenderData(_defenderConfig), _projectilesData, _configRepository.ConfigList);
    }

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        
    }
}
