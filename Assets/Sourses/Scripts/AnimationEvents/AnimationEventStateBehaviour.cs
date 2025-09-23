using UnityEngine;
using UnityEngine.Events;

public class AnimationEventStateBehaviour : StateMachineBehaviour 
{
    [SerializeField] private string _eventName;
    [SerializeField][Range(0f, 1f)] private float _triggerTime;

    public string EventName => _eventName;
    public float TriggerTime => _triggerTime;
}
