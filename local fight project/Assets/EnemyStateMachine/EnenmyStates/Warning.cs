using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Warning : EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
    private Vector3 offset = new Vector3(0, 1, 0);
    private Animator animator;

    private Transform player;
    

    public Warning(EnemyAI enemyStateMachine) : base("Warning", enemyStateMachine) { 
        ai = enemyStateMachine;
        animator = enemyStateMachine._enemyAnimator;
        agent = enemyStateMachine._agent;
        player = enemyStateMachine._player;
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        HandsUpPos();

    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float distance = Vector3.Distance(ai.transform.position, player.position);
        var lookPos = (player.position + offset) - ai.transform.position;
        float angle = Vector3.Angle(lookPos, agent.transform.forward);
        HandsUpPos();
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 5f);
        if(distance <3)
        {
            agent.isStopped = false;
            ai.ChangeState(((EnemyAI)enemyStateMachine)._chase);
        }
        else if (distance > 5)
        {
            agent.isStopped = false;
            ai.ChangeState(((EnemyAI)enemyStateMachine)._patrol);
        }



    }
    private void HandsUpPos() {
        agent.isStopped=true;
        animator.SetInteger("arms", 17);
        animator.SetInteger("legs", 5);
    }
 
}
