using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EnemyGraveDetector : MonoBehaviour
{
    public ReactiveProperty<Grave> CurrentGrave { get; private set; } = new ReactiveProperty<Grave>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Grave coffin))
        {
            CurrentGrave.Value = coffin;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Grave coffin))
        {
            CurrentGrave.Value = null;
        }
    }
}
