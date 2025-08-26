using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitConfig", menuName = "NewUnitConfig / NewConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private Enemy _unitPrefab;
    [SerializeField] private GameObject _hitEffect;

    public GameObject HitEffect => _hitEffect;
    public Enemy UnitPrefab => _unitPrefab;
}
