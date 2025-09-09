using Reflex.Core;
using UnityEngine;

public class LevelInstaller : MonoBehaviour, IInstaller
{
     private SpawnersHandler _unitSpawnerHandler = new SpawnersHandler();

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_unitSpawnerHandler);
    }
}
