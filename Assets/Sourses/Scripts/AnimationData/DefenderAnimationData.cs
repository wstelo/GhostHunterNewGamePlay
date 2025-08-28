using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAnimationData
{
    public static readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
    public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
}
