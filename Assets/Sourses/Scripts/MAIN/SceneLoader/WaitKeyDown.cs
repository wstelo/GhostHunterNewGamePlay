using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitKeyDown : CustomYieldInstruction
{
    public override bool keepWaiting
    {
        get
        {
            return !Input.anyKeyDown;
        }
    }
}
