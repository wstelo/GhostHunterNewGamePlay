using System;
using UniRx;
using UnityEngine;

public class EnemyHealth
{
    public EnemyHealth()
    {
        Count = new ReactiveProperty<int>(0);
    }

    public ReactiveProperty<int> Count { get; private set; }

    public event Action ValueEnded;

    public void Init(int value)
    {
        Count.Value = value;
    }

    public void TakeDamage(int value)
    {
        if (value < 0)
        {
            return;
        }

        Count.Value -= value;
        
        if(Count.Value <= 0)
        {
            ValueEnded?.Invoke();
        }
    }
}
