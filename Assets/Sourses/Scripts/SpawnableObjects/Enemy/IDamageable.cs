using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public List<ElementTypes> ElementTypes { get; }
    Transform Transform { get; }
    public bool IsLastHealth { get; }

    void TakeDamage();
}
