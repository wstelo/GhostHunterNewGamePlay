using UnityEngine;

public class ProjectileButtonAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void StopDisappearanceAnimation()
    {
        _animator.SetBool(ProjectileButtonAnimationData.IsDecreaseScaleAnimation, false);
    }

    public void StartDisappearanceAnimation()
    {
        _animator.SetBool(ProjectileButtonAnimationData.IsDecreaseScaleAnimation, true);
    }
}
