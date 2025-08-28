using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableObject <T> where T :MonoBehaviour
{
    event Action<T> Disabled;

    void Disable();
}