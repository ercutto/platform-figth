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
    private bool lookforSit;
    private GameObject[] sittingpoint;

    public Patrol(EnemyAI enemyStateMachine) : base("patrol", enemyStateMachine) { 
        ai = enemyStateMachine;
        agent = enemyStateMachine._agent;
        player= enemyStateMachine._player;
        aim= enemyStateMachine._aim;
        sittingpoint = enemyStateMachine._sitPoints;
        animator = enemyStateMachine._enemyAnimator;
        speed = enemyStateMachine._speed;
        lookforSit = enemyStateMachine._lookForSit;

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
                DeterminPetrolPos();
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
           

        }
        if(distance <= 1f)
        {
            //Here we can add some enemy actions like pickup object or sit down Etc!
            wait += Time.deltaTime;
            agent.speed = 0;
            animator.SetInteger("arms", 5);
            animator.SetInteger("legs", 5);
            if (wait>maxWait) { wait = 0;
                DeterminAim();
                ChangeAim(ai.random);
                determine_new_aim = false;  }
        }

    }
    void DeterminPetrolPos()
    {
        ai.RandomNum(0, aim.Length);
        ai.aimingto=ai.random.ToString();
        determine_new_aim = true;
    }
    void DeterminAim()
    {
     
            ai.RandomNum(0, 3);
        
        ai.aimingto = ai.random.ToString();
        determine_new_aim = true;
    }
    void ChangeAim(int curentaim)
    {
        if (curentaim == 0)
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
