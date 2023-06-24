using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : EnemyStateMachine
{
    [HideInInspector]
    public Enemy_Idle _idle;
    [HideInInspector]
    public Patrol _patrol;
    [HideInInspector]
    public Chase _chase;

    [HideInInspector]
    public Attack _attack;

    [HideInInspector]
    public GameObject Aim_point;
    [HideInInspector]
    public NavMeshAgent _agent;
    public Transform _player;

    public Animator _enemyAnimator;
    public List<GameObject> way_points = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        
        _agent=GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        GameObject[] waypointsFind = GameObject.FindGameObjectsWithTag("waypoint");
        foreach (GameObject g in waypointsFind)
        {
            way_points.Add(g);
        }
        _idle = new Enemy_Idle(this);
        _patrol = new Patrol(this);
        _chase = new Chase(this);
        _attack = new Attack(this);
    }
    protected override EnemyBase GetInitialState()
    {
        return _idle;
    }
    

}
