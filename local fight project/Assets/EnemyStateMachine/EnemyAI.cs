using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : EnemyStateMachine
{
    //States
    [HideInInspector]
    public Enemy_Idle _idle;
    [HideInInspector]
    public Patrol _patrol;
    [HideInInspector]
    public Chase _chase;
    [HideInInspector]
    public Attack _attack;
    [HideInInspector]
    public Warning _warning;
    //States end

   
    [HideInInspector]
    public NavMeshAgent _agent;
    [HideInInspector]
    public Transform _player;
    [HideInInspector]
    public GameObject[] _aim;
    public Animator _enemyAnimator;
    public float _speed;
   
    // Start is called before the first frame update
    private void Awake()
    {
        
        _agent=GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        _aim = GameObject.FindGameObjectsWithTag("waypoint");
       
        _idle = new Enemy_Idle(this);
        _patrol = new Patrol(this);
        _chase = new Chase(this);
        _attack = new Attack(this);
        _warning = new Warning(this);
    }
    protected override EnemyBase GetInitialState()
    {
        return _idle;
    }
    

}
