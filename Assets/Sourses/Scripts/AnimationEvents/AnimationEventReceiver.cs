using UnityEngine;
using System.Collections.Generic;

public class AnimationEventReceiver : MonoBehaviour 
{
    [Range(0f, 1f)] private float _triggerTime = 0;

    public float TriggerTime => _triggerTime;

    public void SetParameters(float value)
    {
        _triggerTime = value;
        Debug.Log(_triggerTime);
    }
}