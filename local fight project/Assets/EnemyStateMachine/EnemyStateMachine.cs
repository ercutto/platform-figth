using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Device;

public class EnemyStateMachine : MonoBehaviour
{
    
    Rect gui= new Rect(500,10,200,50);
    
    EnemyBase _currentEnemyState;
    public int random;

    void Start()
    {
        _currentEnemyState = GetInitialState();
        if ( _currentEnemyState != null )
        {
            _currentEnemyState.Enter();
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        if ( _currentEnemyState != null )
        {
            _currentEnemyState.UpdateLogic();
        }
    }
    public void ChangeState(EnemyBase newEnemyState)
    {
        _currentEnemyState.Exit();
        _currentEnemyState = newEnemyState;
        _currentEnemyState.Enter();
    }
    protected virtual EnemyBase GetInitialState()
    {
        return null;
    }
    private void OnGUI()
    {
        string content = _currentEnemyState != null ? _currentEnemyState.name : "no current Content";
        GUILayout.BeginArea(gui);
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        GUILayout.EndArea();
    }
    public void RandomNum(int a,int b)
    {
        random = Random.Range(a,b);
    }

}
