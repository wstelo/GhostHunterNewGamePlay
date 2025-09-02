using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderProjectileContainer 
{
    public int ProjectileCount { get; private set; } = 0;

    public event Action ProjectileEnded;
    public event Action<int> CountChanged;

    public void DecreaseCount()
    {
        ProjectileCount--;
        CountChanged?.Invoke(ProjectileCount);

        if (ProjectileCount == 0)
        {
            ProjectileEnded?.Invoke();
        }
    }

    public void SetCount(int count)
    {
        ProjectileCount = count;
    }
}
