using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<AnimationClip> _clips = new List<AnimationClip>();

    private float _defaultAnimatorSpeed = 1;

    public event Action ProjectileSpawnPointEnded;
    public event Action AttackAnimationEnded;

    public void StartIdleAnimation()
    {
        _animator.SetBool(DefenderAnimationData.IsIdle, true);    
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool(DefenderAnimationData.IsIdle, false);
    }

    public void StartAttackAnimation(float attackTime)
    {
        _animator.speed = attackTime;
        _animator.SetBool(DefenderAnimationData.IsAttack, true);
    }

    public void CreateProjectile()
    {
        ProjectileSpawnPointEnded?.Invoke();
    }

    public void EndAttackAnimation()
    {
        _animator.SetBool(DefenderAnimationData.IsAttack, false);
        _animator.speed = _defaultAnimatorSpeed;
        AttackAnimationEnded?.Invoke();
    }

    public float GetAnimationLength(string animationName)
    {
        AnimationClip clip = null;

        foreach (var item in _clips)
        {
            if (item.name.Contains(animationName))
            {
                clip = item;
                break;
            }
        }

        return clip != null ? clip.length : 1f;
    }
}
