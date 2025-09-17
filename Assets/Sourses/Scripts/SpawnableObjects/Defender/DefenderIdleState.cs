using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderIdleState : State
{
    private DefenderAnimatorController _animatorController;
    private DefenceAreaDetector _detector;
    private List<ElementTypes> _currentElement;

    public DefenderIdleState(StateMachine stateMachine, DefenderAnimatorController animator, DefenceAreaDetector detector, List<ElementTypes> currentElement) : base(stateMachine)
    {
        _animatorController = animator;
        _detector = detector;
        _currentElement = currentElement;
    }

    public override void Enter()
    {
        _animatorController.StartIdleAnimation();
    }

    public override void FixedUpdate()
    {
        if (_detector.GetNearbyEnemy(_currentElement) != null)                /////////////////////////////////////////////////////
        {
            StateMachine.SetState<TestAttackState>();
        }
    }

    public override void Exit()
    {
        _animatorController.StopIdleAnimation();
    }
}
