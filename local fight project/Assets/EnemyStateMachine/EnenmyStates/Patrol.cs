using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
    float speed;
    public Transform player;
    private Animator animator;
    bool determine_new_aim=false;
    private GameObject[] aim;
    private float wait = 0;
    private float  maxWait=2f;
    


    public Patrol(EnemyAI enemyStateMachine) : base("patrol", enemyStateMachine) { 
        ai = enemyStateMachine;
        agent = enemyStateMachine._agent;
        player= enemyStateMachine._player;
        aim= enemyStateMachine._aim;
        animator = enemyStateMachine._enemyAnimator;
        speed = enemyStateMachine._speed;
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();

    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //Debug.Log(way_points.Count);
        
        float distance=Vector3.Distance(ai.transform.position, player.position);

        if (distance > 5)
        {
            if (!determine_new_aim)
            {
                DeterminAim();
            }
            if (determine_new_aim)
            {

                    PatrolPos();               
            }
        }
        else
        {
            ai.ChangeState(((EnemyAI)enemyStateMachine)._warning);
        }
        
        
    }
    private void PatrolPos() {
        float distance = Vector3.Distance(ai.transform.position, aim[ai.random].transform.position);

        
        if (distance > 1f)
        {
            agent.speed = speed;
            agent.SetDestination(aim[ai.random].transform.position);
            animator.SetInteger("arms", 1);
            animator.SetInteger("legs", 1);
            Debug.Log(distance);
            
        }
        if(distance <= 1f)
        {
            //Here we can add some enemy actions like pickup object or sit down Etc!
            wait += Time.deltaTime;
            agent.speed = 0;
            animator.SetInteger("arms", 5);
            animator.SetInteger("legs", 5);
            if (wait>maxWait) { wait = 0; determine_new_aim = false; }
        }

    }
    void DeterminAim()
    {
        ai.RandomNum(0, aim.Length);
        ai.aimingto=ai.random.ToString();
        determine_new_aim = true;
    }
   
 
}
