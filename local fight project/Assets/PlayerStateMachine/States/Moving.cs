using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{

    MovementSM msm;
    private int _currentAnim = 1;
    private float _walkSpeedMultiplier = 1.5f;
    private int _punch = 15;
    private int _punch2 = 14;
    //private int _glock = 27;
    private string _arms = "arms";
    private string _legs = "legs";
    public Moving(MovementSM stateMachine) : base("Moving", stateMachine)
    {
        msm = stateMachine;


    }

    public override void Enter()
    {
        base.Enter();
        msm._anim.SetInteger(_arms, _currentAnim);
        msm._anim.SetInteger(_legs, _currentAnim);


    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();


        if (msm._isMovePressed && !msm._isRunPressed)
        {
            msm._characterController.Move(msm._velocity * _walkSpeedMultiplier * Time.deltaTime);
            if (msm._velocity != Vector3.zero)
            {
                msm._characterController.transform.forward = msm._velocity;
            }


            if (msm._isPunchPressed)
            {

                msm._anim.SetInteger(_arms, _punch);

                msm._anim.SetInteger(_legs, _currentAnim);

            }
            else if (msm._isPunch2Pressed)
            {
                msm._anim.SetInteger(_arms, _punch2);

            }
            else
            {
                msm._anim.SetInteger(_arms, _currentAnim);

            }

            msm._anim.SetInteger(_legs, _currentAnim);
        }

        if (msm._velocity == Vector3.zero) { stateMachine.ChangeState(((MovementSM)stateMachine)._idle); }
        else if (msm._isRunPressed && msm._isMovePressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._run); }
        else if (msm._isDuckWalkPressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._duckWalk); }
        

    }

}
