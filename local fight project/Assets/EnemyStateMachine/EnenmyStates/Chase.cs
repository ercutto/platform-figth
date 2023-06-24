using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
    float speed = 1;
    public Transform player;
    

    public Chase(EnemyAI enemyStateMachine) : base("Chase", enemyStateMachine) { 
        ai = enemyStateMachine;
        agent = enemyStateMachine._agent;
        player= enemyStateMachine._player;
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();

        ChasePlayer(player.position);

    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float distance=Vector3.Distance(ai.transform.position, player.position);
        if (distance<3)
        {
            ChasePlayer(player.position);
            if (distance < 2f)
            {
                ai.ChangeState(((EnemyAI)enemyStateMachine)._attack);
            }
        }
        else
        {
            ai.ChangeState(((EnemyAI)enemyStateMachine)._patrol);
        }

       

    }
    private void ChasePlayer(Vector3 to) {
        agent.SetDestination(to);
        agent.speed = speed;
        ai._enemyAnimator.SetInteger("arms", 1);
        ai._enemyAnimator.SetInteger("legs", 1);
    }
 
}
