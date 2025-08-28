using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void StartIdleAnimation()
    {
        _animator.SetBool(DefenderAnimationData.IsIdle, true);
       
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool(DefenderAnimationData.IsIdle, false);
    }

    public void StartAttackAnimation()
    {
        _animator.SetBool(DefenderAnimationData.IsAttack, true);
    }

    public void StopAttackAnimation()
    {
        _animator.SetBool(DefenderAnimationData.IsAttack, false);
    }
}
