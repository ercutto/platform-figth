using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.AnimatedValues;
using UnityEditor.ShaderKeywordFilter;
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
    [HideInInspector]
    public Sitting _sitting;
    [HideInInspector]
    public Pickup _pickup;

    //States end
    //vectors
    [HideInInspector]
    public Vector3 _sitting_position = new Vector3(0, 0, 0);
    [HideInInspector]
    public Vector3 _sitting_Rotation = new Vector3(180, -90, -90);
    [HideInInspector]
    public Vector3 _pickup_position = new Vector3(0, 0, 0);
    [HideInInspector]
    public Vector3 _pickup_Rotation = new Vector3(180, -90, 0);
    //vectors end
    [HideInInspector]
    public bool _lookForSit = false;
    [HideInInspector]
    public bool _lookForpickup = false;

    

    [HideInInspector]
    public NavMeshAgent _agent;
    [HideInInspector]
    public Transform _player;
    [HideInInspector]
    public GameObject[] _aim;
    
    public GameObject[] _sitPoints;
    public List<GameObject> _sitPointList;
    [HideInInspector]
    public GameObject[] _pickupPoints;
    [HideInInspector]
    public List<GameObject> _pickuplist;

    public Animator _enemyAnimator;
    public float _speed;
    public List<bool> _objectBool;

    public GameObject[] _enemyAlies;
    public List<GameObject> _enemyAliesList;
    

    private bool empty;
    // Start is called before the first frame update
    private void Awake()
    {    
        RemoveAll(ref _aim);
        RemoveAll(ref _sitPoints);
        RemoveAll(ref _pickupPoints );

        _agent =GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        _aim = GameObject.FindGameObjectsWithTag("waypoint");
        _sitPoints = GameObject.FindGameObjectsWithTag("sittingpoint");
        _pickupPoints = GameObject.FindGameObjectsWithTag("pickuppoint");
        _enemyAlies = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject g in _pickupPoints)
        {
            _pickuplist.Add(g);
        }
        foreach(GameObject g in _sitPoints)
        {
            _sitPointList.Add(g);
        }
        foreach (GameObject g in _enemyAlies)
        {
            _enemyAliesList.Add(g);
        }

        _idle = new Enemy_Idle(this);
        _patrol = new Patrol(this);
        _chase = new Chase(this);
        _attack = new Attack(this);
        _warning = new Warning(this);
        _sitting= new Sitting(this);
        _pickup = new Pickup(this);

    }
    protected override EnemyBase GetInitialState()
    {
        return _idle;
    }
    //Clearing Elemnts Of array;
   
    //public void RemoveElement<T>(ref T[] arr, int index)
    //{
        
    //    for (int i = 0; i < arr.Length -1; i++)
    //    {
    //        arr[i] = arr[i + 1];

    //    }

    //    Array.Resize(ref arr, arr.Length - 1);
    //}
    public void RemoveAll<T>(ref T[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            //arr[i] = arr[i + 1];
            arr[i]=arr[i+1];
            Array.Resize(ref arr, arr.Length - 1);
        }

        
    }
   


}



