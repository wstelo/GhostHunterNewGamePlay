using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitConfig", menuName = "NewUnitConfig / NewConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private EnemyTypes _enemyType;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private GameObject _hitEffect;

    public EnemyTypes EnemyType => _enemyType;
    public GameObject HitEffect => _hitEffect;
    public Enemy Prefab => _prefab;
}
