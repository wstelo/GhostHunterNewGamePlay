using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointDetector : MonoBehaviour
{
    public event Action Detected;
    public event Action Destroyed;

    public void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy _))
        {
            Detected?.Invoke();
        }
    }
}
