using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : Defender
{
    public override DefenderTypes DefenderType => DefenderTypes.Magician;
    public override List<ProjectileTypes> ProjectilesTypes => new List<ProjectileTypes> { ProjectileTypes.StandartMagicianProjectile };
    public override DefenderAttackTypes AttackType => DefenderAttackTypes.StandartAttack;
}
