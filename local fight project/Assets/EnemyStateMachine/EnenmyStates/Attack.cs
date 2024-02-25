using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class  Attack : EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
    
    public Transform player;
    private Vector3 offset = new Vector3(0, 1, 0);
    float count= 0;
    float speed;
   
   


    public Attack(EnemyAI enemyStateMachine) : base("Attack", enemyStateMachine)
    { 
        ai = enemyStateMachine;
        agent = enemyStateMachine._agent;
        player= enemyStateMachine._player;
        speed = enemyStateMachine._speed;
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
       
        AttackToPlayer();

    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float distance=Vector3.Distance(ai.transform.position, player.position);
        
      
        AttackToPlayer();
        
        
        if (distance > 2f)
        {
            ai.ChangeState(((EnemyAI)enemyStateMachine)._chase);
        }

       
    }
    private void AttackToPlayer() {
        var lookPos = (player.position+offset) - ai.transform.position;
        float angle = Vector3.Angle(lookPos, agent.transform.forward);
        
        agent.transform.rotation=Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(lookPos),Time.deltaTime*5f);

        agent.speed = speed;


        count += Time.deltaTime;
        
        if (count > 2)
        {
            ai.RandomNum(1, 3);
            
            SelectFightMode(ai.random);
        }
        if (count > 2) { count = 0; }


        Debug.Log(angle);
       

        if (agent.velocity==Vector3.zero)
        {

            if (angle > 10f)
            {
                ai._enemyAnimator.SetInteger("legs", 1);
            }
            else
            {
                ai._enemyAnimator.SetInteger("legs", 5);
            }
            
           
        }
        else
        {
            
             

            ai._enemyAnimator.SetInteger("legs", 1);

        }

    }
    void SelectFightMode(int figthMode)
    {
        if (figthMode == 1) { Ducking(); }
        else if (figthMode == 2) { Punchcing(); }
        else if(figthMode == 3) { LeftPunch(); }
    }
    void Ducking()
    {
        ai.attackType = "defense";
        ai._enemyAnimator.SetInteger("arms", 36);
       
    }
    void Punchcing()
    {
        ai.attackType = "RightPunch";
        ai._enemyAnimator.SetInteger("arms", 15);
    }
    void LeftPunch()
    {
        ai.attackType = "LeftPunch";
        ai._enemyAnimator.SetInteger("arms", 14);
    }

    

}
