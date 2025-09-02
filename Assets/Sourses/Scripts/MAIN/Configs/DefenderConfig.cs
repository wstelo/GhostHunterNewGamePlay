using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "NewCharacterConfig / NewConfig")]
public class DefenderConfig : ScriptableObject
{
    [SerializeField] private DefenderAttackTypes _attackType;
    [SerializeField] private DefenderTypes _defenderType;
    [SerializeField] private BuildPreview _unitPreviewPrefab;
    [SerializeField] private Defender _prefab;
    [SerializeField] private ProjectileTypes _projectileType;
    [SerializeField] private GameObject _hitEffect;

    public DefenderAttackTypes AttackTypes => _attackType;
    public DefenderTypes DefenderType => _defenderType;
    public GameObject HitEffect => _hitEffect;
    public BuildPreview UnitPreviewPrefab => _unitPreviewPrefab;
    public Defender Prefab => _prefab;   
    public ProjectileTypes ProjectileTypes => _projectileType;
}
