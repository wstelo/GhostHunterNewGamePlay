using UnityEngine;

public class DefenderData : SpawnableObjectData<Defender>
{
    public BuildPreview BuildPreview { get; private set; }
    public Projectile ProjectilePrefab { get; private set; }
    public GameObject HitEffect { get; private set; }

    public DefenderData(ElementTypes type, Defender prefab, Color typeColor, BuildPreview previewPrefab, Projectile projectilePrefab, GameObject _hitEffect)
    {
        Inittialize(type, typeColor, prefab);
        BuildPreview = previewPrefab;
        ProjectilePrefab = projectilePrefab;
        HitEffect = _hitEffect;
    }
}
