using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
        InitializeSubSttate(); }
    public override void EnterState()
    {
        HandleJump();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        HandleGravity();

    }
    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
        if (Ctx.IsJumpPressed) { Ctx.RequireNewJumpPress = true; }
        
          
        Ctx.CurrentJumpResetCoroutine = Ctx.StartCoroutine(IJumpResetRoutine());
        if (Ctx.JumpCount == 3)
        {
            Ctx.JumpCount = 0;
            Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
        }

    }

    public override void InitializeSubSttate()
    {

    }
    public override void CheckSwitchState()
    {
      if(Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    IEnumerator IJumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        Ctx.JumpCount = 0;
    }

    void HandleJump()
    {
        if (Ctx.JumpCount < 3 && Ctx.CurrentJumpResetCoroutine != null)
        {
            Ctx.StopCoroutine(Ctx.CurrentJumpResetCoroutine);
        }
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
        Ctx.IsJumping = true;
        Ctx.JumpCount+=1;
        Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocities[Ctx.JumpCount];
        Ctx.ApliedMovementY = Ctx.InitialJumpVelocities[Ctx.JumpCount];

    }
    void HandleGravity()
    {
        bool isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;
        float fallMultiplier = 2.0f;

        if (isFalling)
        {
            float previousVelocity=Ctx.CurrentMovementY;
           
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.JumpGravities[Ctx.JumpCount]*fallMultiplier*Time.deltaTime);
            Ctx.ApliedMovementY=Mathf.Max((previousVelocity+Ctx.CurrentMovementY)*.5f,-20.0f);
        }
        else
        {
            float previousVelocity = Ctx.CurrentMovementY;

            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.JumpGravities[Ctx.JumpCount] *Time.deltaTime);
            Ctx.ApliedMovementY = Mathf.Max((previousVelocity + Ctx.CurrentMovementY) * .5f);

        }
    }
}
