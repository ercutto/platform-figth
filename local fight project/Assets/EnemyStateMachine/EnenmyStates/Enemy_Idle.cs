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
    
    bool passTopAtrol=false;
    float count = 0;

    public Enemy_Idle(EnemyAI enemyStateMachine) : base("enemyIdle", enemyStateMachine) { 
        ai = enemyStateMachine;
        animator = enemyStateMachine._enemyAnimator;
        agent = enemyStateMachine._agent;
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
        if(count > 2f) { passTopAtrol = true; }
        if(!passTopAtrol)
        {
            IdlePos();
        }
        else
        {
            ai.ChangeState(((EnemyAI)enemyStateMachine)._patrol);
        }


        
    }
    private void IdlePos() {
        animator.SetInteger("arms", 5);
        animator.SetInteger("legs", 5);
    }
 
}
