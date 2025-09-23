using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class AnimationEventReceiver : MonoBehaviour 
{
    private Animator _animator;

    private List<AnimationEventStateBehaviour> _behaviours = new List<AnimationEventStateBehaviour>();

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            for(int i = 0;i < _animator.layerCount;i++)
            {
                var stateBehaviours = _animator.GetBehaviours<AnimationEventStateBehaviour>();
                _behaviours.AddRange(stateBehaviours);
            }
        }     
    }

    public float GetAnimationTriggerTime(string _eventName)
    {
        if(_behaviours.Count == 0)
        {
            return default(float);
        }

        foreach (var behaviour in _behaviours)
        {
            if (behaviour.EventName == _eventName)
            {
                return behaviour.TriggerTime;
            }
        }

        return default(float);
    }
}