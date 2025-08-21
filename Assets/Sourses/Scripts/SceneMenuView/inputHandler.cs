using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const int LeftMouseButtonIndex = 0;
    private const int RightMouseButtonIndex = 1;

    public event Action SelectButtonPressed;
    public event Action CancelButtonUnpressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButtonIndex))
        {
            SelectButtonPressed?.Invoke();
        }

        if (Input.GetMouseButtonUp(LeftMouseButtonIndex))
        {
            CancelButtonUnpressed?.Invoke();
        }
    }
}
