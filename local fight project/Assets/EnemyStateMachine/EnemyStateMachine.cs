using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Device;

public class EnemyStateMachine : MonoBehaviour
{
    
    Rect gui= new Rect(500,10,200,50);
    Rect gui2= new Rect(500,60,400,50);
    Rect aim= new Rect(500,120,400,50);

    EnemyBase _currentEnemyState;
    [HideInInspector]
    public int random;
    [HideInInspector] 
    public string attackType = "no attack";
    [HideInInspector]
    public string aimingto = "no aim";


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
        GUILayout.BeginArea(gui2);
        GUILayout.Label($"<color='black'><size=40>{attackType}</size></color>");
        GUILayout.EndArea();
        GUILayout.BeginArea(aim);
        GUILayout.Label($"<color='black'><size=40>{aimingto}</size></color>");
        GUILayout.EndArea();

    }
   
    public void RandomNum(int a,int b)
    {
        random = Random.Range(a,b);
    }

   
}
