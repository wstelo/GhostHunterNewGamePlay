using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "NewCharacterConfig / NewConfig")]
public class DefenderConfig : ScriptableObject
{
    [SerializeField] private DefenderTypes _defenderType;
    [SerializeField] private BuildPreview _unitPreviewPrefab;
    [SerializeField] private Defender _prefab;
    [SerializeField] private List<ProjectileTypes> _projectileTypes;
    [SerializeField] private GameObject _hitEffect;

    public DefenderTypes DefenderType => _defenderType;
    public GameObject HitEffect => _hitEffect;
    public BuildPreview UnitPreviewPrefab => _unitPreviewPrefab;
    public Defender Prefab => _prefab;   
    public List<ProjectileTypes> ProjectileTypes => _projectileTypes;
}
