using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
    BaseState _currentState;
    // Start is called before the first frame update
    void Start()
    {
        _currentState = GetInitialState();
        if( _currentState != null)
        {
            _currentState.Enter();
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateLogic();
        }
    }
    private void LateUpdate()
    {
        if (_currentState != null)
        {
            _currentState.UpdatePhysics();
        }
    }
    public void ChangeState(BaseState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
    protected virtual BaseState GetInitialState()
    {
        return null;
    }
    private void OnGUI()
    {
        string content = _currentState != null ? _currentState.name : "no current Content";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
