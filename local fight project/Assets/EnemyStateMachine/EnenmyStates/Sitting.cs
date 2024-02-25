using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class Sitting : EnemyBase
{
    protected EnemyAI ai;
    protected NavMeshAgent agent;
    float speed;
    public Transform player;
    private Animator animator;
    bool determine_new_aim=false;
    private GameObject[] aim;
    private GameObject[] sittingpoint;
    private List<GameObject> sitpointlist;
    private float wait = 0;
    private float  maxWait=4f;
    private Vector3 sitting_position;
    private Vector3 sitting_Rotation;
    protected GameObject targetChair;
    protected Vector3 target;
   
    



    public Sitting(EnemyAI enemyStateMachine) : base("Sitting", enemyStateMachine) { 
        ai = enemyStateMachine;
        agent = enemyStateMachine._agent;
        player= enemyStateMachine._player;
        aim= enemyStateMachine._aim;
        //sittingpoint = enemyStateMachine._sitPoints;
        animator = enemyStateMachine._enemyAnimator;
        speed = enemyStateMachine._speed;
       
        sitting_Rotation = enemyStateMachine._sitting_Rotation;
        
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
                DeterminChair();
            }
            if (determine_new_aim)
            {
                    SitPos();               
            }
        }
        else
        {
            agent.enabled = true;
            ai.ChangeState(((EnemyAI)enemyStateMachine)._warning);
        }
        
        
    }
    private void SitPos() {
       
        float distance = Vector3.Distance(ai.transform.position, target);
        agent.enabled = true;
        if (distance > 1f)
        {
            agent.speed = speed;
            agent.SetDestination(target);
            animator.SetInteger("arms", 1);
            animator.SetInteger("legs", 1);
            targetChair.tag = "Untagged";

        }
        if (distance <= 1f)
        {
           
        
            ai.transform.parent = targetChair.transform;
            agent.enabled = false;
            //Here we can add some enemy actions like pickup object or sit down Etc!

            agent.speed = 0;
            animator.SetInteger("arms", 3);
            animator.SetInteger("legs", 3);
            ai.transform.localPosition = sitting_position;
            ai.transform.localEulerAngles = sitting_Rotation;
            wait += Time.deltaTime;
            if (wait > maxWait)
            {
                
                ai.transform.parent = null;
                agent.enabled = true;
                targetChair.tag = "sittingpoint";
                determine_new_aim = false;
                wait = 0;
               


            }

        }
            

           
        

        

    }

    void DeterminChair()
    {
        sittingpoint = GameObject.FindGameObjectsWithTag("sittingpoint");
      
           
        
        
        
       
        
        //ai.RandomTow(0, sitpointlist.Count);
        targetChair = sittingpoint[ai.random];
        

        RemoveElement(ref sittingpoint, ai.random);
        target = targetChair.transform.position;
        ai.aimingto = ai.random.ToString();

        determine_new_aim = true;



    }
    public void RemoveElement<T>(ref T[] arr, int index)
    {

        for (int i = 0; i < arr.Length - 1; i++)
        {
            arr[i] = arr[i + 1];

        }

        Array.Resize(ref arr, arr.Length - 1);
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
