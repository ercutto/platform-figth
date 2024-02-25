using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pickup: EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
    float speed;
    public Transform player;
    private Animator animator;
    bool determine_new_aim=false;
   
   
    private float wait = 0;
    private float  maxWait=4f;
    private Vector3 pickup_position;
    private Vector3 pickup_Rotation;
    protected Vector3 target;
    


    public Pickup
        (EnemyAI enemyStateMachine) : base("Pickup", enemyStateMachine) { 
        ai = enemyStateMachine;
        agent = enemyStateMachine._agent;
        player= enemyStateMachine._player;
        animator = enemyStateMachine._enemyAnimator;
        speed = enemyStateMachine._speed;
       
        pickup_Rotation = enemyStateMachine._pickup_Rotation;
        pickup_Rotation= enemyStateMachine._pickup_Rotation;
       
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
                DeterminNewPickup();
            }
            if (determine_new_aim)
            {
                PickupPos();
            }
          

        }
        else
        {
            ai.ChangeState(((EnemyAI)enemyStateMachine)._warning);
        }

       
    }
    private void PickupPos() {
        
        target = ai._pickuplist[ai.randomTwo].transform.position; 
        float distance = Vector3.Distance(ai.transform.position, target);
        

        if (distance > 1f)
        {
            agent.speed = speed;
            agent.SetDestination(target);
            animator.SetInteger("arms", 1);
            animator.SetInteger("legs", 1);
            

        }
        if (distance <= 1f)
        {

            //Here we can add some enemy actions like pickup object or sit down Etc!
            
            agent.speed = 0;
            animator.SetInteger("arms", 32);
            animator.SetInteger("legs", 32);
           
            wait += Time.deltaTime;

            if (wait > maxWait)
            {
                
                wait = 0;
                DeterminAim();
                ChangeAim(ai.random);
                ai._pickuplist.RemoveAt(ai.randomTwo);
                determine_new_aim = false;
                
            }
        }

    }

    void DeterminNewPickup()
    {
        
       
      
        if (ai._pickuplist.Count != 0)
        {
            ai.RandomTow(0, ai._pickuplist.Count);

            ai.aimingto = ai.randomTwo.ToString();

            determine_new_aim = true;
        }
        else 
        {
            determine_new_aim = false;
            ChangeAim(1);
        }

    }
     void DeterminAim()
    {
       
        ai.RandomNum(0,3);
        ai.aimingto=ai.random.ToString();
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
