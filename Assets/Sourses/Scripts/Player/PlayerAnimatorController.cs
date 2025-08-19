using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void StartIdleAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsIdle, true);
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsIdle, false);
    }

    public void StartAttackAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsAttack, true);
    }

    public void StopAttackAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsAttack, false);
    }
}
