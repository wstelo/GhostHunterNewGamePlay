using Reflex.Core;
using UnityEngine;
using UnityEngine.Splines;

public class LevelInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(SpawnersHandler));
    }
}
