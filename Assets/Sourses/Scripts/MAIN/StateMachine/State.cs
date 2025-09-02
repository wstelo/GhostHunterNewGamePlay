using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected readonly StateMachine StateMachine;

    public State(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}
