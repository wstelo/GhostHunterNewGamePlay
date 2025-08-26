using System.Collections;
using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private ConfigsRepository configRepository;

    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton(configRepository);
    }
}
