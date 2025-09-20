using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderIdleState : State
{
    private DefenderAnimatorController _animatorController;
    private DefenceAreaDetector _detector;
    private List<ElementTypes> _currentElement;
    private Enemy _currentTarget = null;

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
        _currentTarget = _detector.GetNearbyEnemy(_currentElement);

        if (_currentTarget != null && _currentTarget.IsMarked == false)                /////////////////////////////////////////////////////
        {
            StateMachine.SetState<TestAttackState>();
        }
    }

    public override void Exit()
    {
        _animatorController.StopIdleAnimation();
    }
}
