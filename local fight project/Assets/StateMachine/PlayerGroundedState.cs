using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    { 
        IsRootState = true;
        InitializeSubSttate(); }
    public override void EnterState()
    {
        // Debug.Log("grounded");
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
        Ctx.ApliedMovementY=Ctx.GroundedGravity;
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {

    }

    public override void InitializeSubSttate()
    {
        if(!Ctx.IsMovementPressed&&!Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }else if(Ctx.IsMovementPressed&&!Ctx.runInEditMode)
        {
            SetSubState(Factory.Walk());

        }
        else
        {
            SetSubState(Factory.Run());
        }
    }
    public override void CheckSwitchState()
    {
        if(Ctx.IsJumpPressed&&!Ctx.RequireNewJumpPress) { SwitchState(Factory.Jump()); }
    }




}
