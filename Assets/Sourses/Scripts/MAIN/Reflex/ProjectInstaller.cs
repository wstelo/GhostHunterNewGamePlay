using System.Collections;
using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private ConfigsRepository _configRepository;
    [SerializeField] private DefenderConfig _currentDefenderConfig;

    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton(_configRepository);
        builder.AddSingleton(_currentDefenderConfig);
    }
}
