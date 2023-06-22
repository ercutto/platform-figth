using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ducking : BaseState
{

    MovementSM msm;
    private int _currentAnim = 36;
    private int _currentArm = 6;
    //private int _punch = 15;
    private string _arms = "arms";
    private string _legs = "legs";
    public Ducking(MovementSM stateMachine) : base("Ducking", stateMachine)
    {
        msm = stateMachine;
  
        
    }
    
    public override void Enter()
    {
        base.Enter();
        msm._anim.SetInteger(_legs,_currentAnim);
        msm._anim.SetInteger(_arms,_currentArm);

       
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        
        
        msm._anim.SetInteger(_legs, _currentAnim);
        msm._anim.SetInteger(_arms, _currentArm);
        msm._characterController.Move(msm._velocity * Time.deltaTime);
        if (msm._velocity != Vector3.zero)
        {
            msm._characterController.transform.forward = msm._velocity;
        }

        if (msm._isMovePressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._duckWalk); }
        //if (msm._isMovePressed && !msm._isRunPressed) {
        //    msm._characterController.Move(msm._velocity * Time.deltaTime); 
        //    msm._characterController.transform.forward=msm._velocity;
        //    if (msm._isPunchPressed)
        //    {
        //        msm._anim.SetInteger(_arms, _punch);

        //    }
        //    else
        //    {
        //        msm._anim.SetInteger(_arms, _currentAnim);

        //    }

        //    msm._anim.SetInteger(_legs,_currentAnim);
        //}

        //if (msm._velocity == Vector3.zero) { stateMachine.ChangeState(((MovementSM)stateMachine)._idle); }
        //else if (msm._isRunPressed && msm._isMovePressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._run); }

    }

}
