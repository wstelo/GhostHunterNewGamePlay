using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderProjectileContainer : IRechargeable
{
    public int Count { get; private set; } = 0;

    public event Action ProjectileEnded;
    public event Action<int> CountChanged;

    public void DecreaseCount()
    {
        Count--;
        CountChanged?.Invoke(Count);

        if (Count == 0)
        {
            ProjectileEnded?.Invoke();
        }
    }

    public void Recharge(int count)
    {
        if(Count >= 0)
        {
            Count += count;
            CountChanged?.Invoke(Count);
        }
    }

    public void Clear()
    {
        Count = 0;
    }
}
