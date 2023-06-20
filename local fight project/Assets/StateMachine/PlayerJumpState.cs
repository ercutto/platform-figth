using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        HandleJump();
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {

    }

    public override void InitializeSubSttate()
    {

    }
    public override void CheckSwitchState()
    {

    }
    void HandleJump()
    {

    }
}
