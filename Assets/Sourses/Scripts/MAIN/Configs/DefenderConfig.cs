using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "NewCharacterConfig / NewConfig")]
public class DefenderConfig : ScriptableObject
{
    [SerializeField] private BuildPreview _unitPreviewPrefab;
    [SerializeField] private Defender _prefab;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private GameObject _hitEffect;

    public Projectile ProjectilePrefab => _projectilePrefab;
    public GameObject HitEffect => _hitEffect;
    public BuildPreview UnitPreviewPrefab => _unitPreviewPrefab;
    public Defender Prefab => _prefab;
}
