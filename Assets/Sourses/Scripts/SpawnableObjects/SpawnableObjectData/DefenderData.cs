using System.Collections.Generic;
using UnityEngine;

public class DefenderData : SpawnableObjectData<Defender>
{
    public ProjectileTypes ProjectileType {  get; private set; }
    public DefenderAttackTypes AttackTypes { get; private set; }
    public DefenderTypes DefenderType { get; private set; }
    public GameObject HitEffect { get; private set; }

    public DefenderData(DefenderTypes defenderTypes, Defender prefab, GameObject _hitEffect) ///// лишние поля
    {
        Inittialize(prefab);
        DefenderType = defenderTypes;
        HitEffect = _hitEffect;
    }
}
