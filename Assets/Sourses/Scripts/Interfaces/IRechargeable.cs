using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRechargeable                               //////////////////////////// мюусъ нм рср бннаые?
{
    public event Action ProjectileEnded;
    public event Action<int> CountChanged;

    public int Count { get; }

    void Recharge(int count);
    void DecreaseCount();
    void Clear();
}
