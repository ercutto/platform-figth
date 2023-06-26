using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    protected MovementSM msm;
    private int _currentAnim=5;
    private int _punch=15;
    private int _punch2=14;
    //private int _glock = 27;
    private string _arms = "arms";
    private string _legs = "legs";
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine)
    {
        msm= stateMachine;
   
    }
    
    public override void Enter()
    {
        base.Enter();
        msm._anim.SetInteger(_arms,_currentAnim);
        msm._anim.SetInteger(_legs,_currentAnim); ;




    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!msm._isMovePressed) {
            if (msm._isPunchPressed)
            {
                msm._anim.SetInteger(_arms, _punch);
                msm._anim.SetInteger(_legs, _currentAnim);
            }
            else if (msm._isPunch2Pressed)
            { msm._anim.SetInteger(_arms, _punch2); }
            else { msm._anim.SetInteger(_arms, _currentAnim); }
           
            msm._anim.SetInteger(_legs, _currentAnim);
        }
        if(msm._velocity != Vector3.zero&&!msm._isDuckWalkPressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._moving); }
        else if (msm._velocity == Vector3.zero && msm._isDuckWalkPressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._ducking); }
        else if (msm._velocity != Vector3.zero && !msm._isDuckWalkPressed) { stateMachine.ChangeState(((MovementSM)stateMachine)._duckWalk); }
        
    }
   
}
