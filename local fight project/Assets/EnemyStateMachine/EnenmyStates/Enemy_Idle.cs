using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Idle : EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
   
    private Animator animator;
    
    bool passTopatrol=false;
    float count = 0;
    //bool patrolling;
    //bool lookingforSit;
    //bool lookingforpickup;
    bool determine_new_aim;
    private GameObject[] aim;
    

    public Enemy_Idle(EnemyAI enemyStateMachine) : base("enemyIdle", enemyStateMachine) { 
        ai = enemyStateMachine;
        animator = enemyStateMachine._enemyAnimator;
        agent = enemyStateMachine._agent;
        //lookingforSit = enemyStateMachine._lookForSit;
        //lookingforpickup = enemyStateMachine._lookForpickup;
        aim= enemyStateMachine._aim;
        
       
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        IdlePos();

    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
        count+=Time.deltaTime;
        if(count > 2f) { DeterminAim(); passTopatrol = true;  }
        if(!passTopatrol)
        {
            IdlePos();
        }
        else
        {

            if(!determine_new_aim)
            {
                DeterminAim();
                
            }else if (determine_new_aim)
            {
                ChangeAim(ai.random);
            }
            


        }


        
    }
    private void IdlePos() {
        animator.SetInteger("arms", 5);
        animator.SetInteger("legs", 5);
    }

    void DeterminAim()
    {
            ai.RandomNum(0, 3);

        ai.aimingto = ai.random.ToString();
        determine_new_aim = true;
    }
    void ChangeAim(int curentaim)
    {
        if (curentaim==0)
        {
           ai.ChangeState(((EnemyAI)enemyStateMachine)._patrol);
        }
        else if (curentaim == 1)
        {
            
            ai.ChangeState(((EnemyAI)enemyStateMachine)._sitting);
        }
        else if (curentaim == 2)
        {
            
            ai.ChangeState(((EnemyAI)enemyStateMachine)._pickup);
        }
        
    }


}
