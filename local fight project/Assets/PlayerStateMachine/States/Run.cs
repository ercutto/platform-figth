using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : BaseState
{

    MovementSM msm;
    private int _currentAnim = 2;
    private int _punch = 15;
    private int _punch2 = 15;
    //private int _glock = 27;
    private float _runSpeedMultiplier = 3f;
    private string _arms = "arms";
    private string _legs = "legs";
    public Run(MovementSM stateMachine) : base("Run", stateMachine)
    {
        msm = stateMachine;
  
        
    }
    
    public override void Enter()
    {
        base.Enter();
        msm._anim.SetInteger(_arms,_currentAnim);
        msm._anim.SetInteger(_legs,_currentAnim);

        
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

           
        if (msm._isPunchPressed) {           
            msm._anim.SetInteger(_arms, _punch);
            msm._anim.SetInteger(_legs, _currentAnim);
        }
        else if (msm._isPunch2Pressed)
        {
            msm._anim.SetInteger(_arms, _punch2);
            msm._anim.SetInteger(_legs, _currentAnim);
        }
        else if(!msm._isPunchPressed&& msm._isPunch2Pressed)
        {
            msm._anim.SetInteger(_arms, _currentAnim);
            msm._anim.SetInteger(_legs, _currentAnim);
        }

        msm._characterController.Move(_runSpeedMultiplier * Time.deltaTime * msm._velocity);
        if (msm._velocity != Vector3.zero)
        {
            msm._characterController.transform.forward = msm._velocity;
        }

        if (!msm._isRunPressed)
        {
            if (!msm._isMovePressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._idle); }
            else { stateMachine.ChangeState(((MovementSM)stateMachine)._moving); }
        }
        

       // if (msm._v == Vector3.zero) { stateMachine.ChangeState(((MovementSM)stateMachine)._idle); }

    }
   
}
