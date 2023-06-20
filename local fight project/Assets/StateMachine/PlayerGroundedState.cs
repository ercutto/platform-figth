using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
       // Debug.Log("grounded");
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
        if(_ctx.IsJumpPressed) { SwitchState(_factory.Jump()); }
    }




}
