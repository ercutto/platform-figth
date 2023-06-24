using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
    float speed = 1;
    public Transform player;

    public Patrol(EnemyAI enemyStateMachine) : base("patrol", enemyStateMachine) { 
        ai = enemyStateMachine;
        agent = enemyStateMachine._agent;
        player= enemyStateMachine._player;
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();

        PatrolPos(Vector3.zero);

    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float distance=Vector3.Distance(ai.transform.position, player.position);
        if (distance>3)
        {
            PatrolPos(Vector3.zero);
        }
        else
        {
            ai.ChangeState(((EnemyAI)enemyStateMachine)._chase);
        }
        
        
    }
    private void PatrolPos(Vector3 to) {
        agent.SetDestination(to);
        agent.speed = speed;
        ai._enemyAnimator.SetInteger("arms", 1);
        ai._enemyAnimator.SetInteger("legs", 1);
    }
 
}
