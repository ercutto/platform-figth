using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementSM : StateMachine
{
    //states
    [HideInInspector]
    public Idle _idle;
    [HideInInspector]
    public Moving _moving;
    [HideInInspector]
    public Run _run;
    [HideInInspector]
    public DuckWalk _duckWalk;
    [HideInInspector]
    public Ducking _ducking;


    [HideInInspector]
    public inputs _inputs;
    
    [HideInInspector]
    public Vector3 _velocity;
    //dont use in states
    private Vector3 _velocityInput;
    //movement bools
    [HideInInspector]
    public bool _isMovePressed;
    [HideInInspector]
    public bool _isPunchPressed;
    [HideInInspector]
    public bool _isPunch2Pressed;
    [HideInInspector]
    public bool _isRunPressed;
    [HideInInspector]
    public bool _isDuckWalkPressed;
    [HideInInspector]
    public bool _isDuckingPressed;
 
    [HideInInspector]
    public CharacterController _characterController;
    public Animator _anim;
    private void Awake()
    {
        //states implimentations
        _inputs=new inputs();
        _idle = new Idle(this);
        _moving = new Moving(this);
        _run = new Run(this);
        _duckWalk = new DuckWalk(this);
        _ducking = new Ducking(this);

        

        _characterController=GetComponent<CharacterController>();
        _inputs.PlayerInputs.move.performed += OnMovePerformed;
        _inputs.PlayerInputs.move.canceled += OnMoveCanceled;
        _inputs.PlayerInputs.Punch.performed += OnPunchPerformed;
        _inputs.PlayerInputs.Punch.canceled += OnPunchCanceled;
        _inputs.PlayerInputs.Punch2.performed += OnPunch2Performed;
        _inputs.PlayerInputs.Punch2.canceled += OnPunch2Canceled;
        _inputs.PlayerInputs.Run.performed += OnRunPerformed;
        _inputs.PlayerInputs.Run.canceled += OnRunCanceled;
        _inputs.PlayerInputs.DuckWalk.performed += OnDuckWalkPerformed;
        _inputs.PlayerInputs.DuckWalk.canceled += OnDuckWalkCanceled;
    }
  



    protected override BaseState GetInitialState() {

        return _idle;
    }
    public void OnEnable()
    {
        _inputs.Enable();
    }
    public void OnDisable()
    {
        _inputs.Disable();
    }
    #region Inputs
    //move
    public void OnMovePerformed(InputAction.CallbackContext context)
    {
        _velocityInput = context.ReadValue<Vector2>();
        _velocity = new Vector3(_velocityInput.x,0,_velocityInput.y );
        _isMovePressed = true;
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _velocity = Vector3.zero;
        _isMovePressed = false;
    }
    //puch
    private void OnPunchPerformed(InputAction.CallbackContext context)
    {
        _isPunchPressed = context.ReadValueAsButton();
        _isPunch2Pressed=false;
    }
    private void OnPunchCanceled(InputAction.CallbackContext context)
    {
        _isPunchPressed=context.ReadValueAsButton();
    }
    //punch2
    public void OnPunch2Performed(InputAction.CallbackContext context)
    {
        _isPunch2Pressed = context.ReadValueAsButton();
        _isPunchPressed = false;
    }
    public void OnPunch2Canceled(InputAction.CallbackContext context)
    {
        _isPunch2Pressed = context.ReadValueAsButton();


    }

    //run
    public void OnRunPerformed(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();

    }
    public void OnRunCanceled(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }
    //DuckWalk
    private void OnDuckWalkPerformed(InputAction.CallbackContext context)
    {
        _isDuckWalkPressed = context.ReadValueAsButton();
    }
    private void OnDuckWalkCanceled(InputAction.CallbackContext context)
    {
        _isDuckWalkPressed = context.ReadValueAsButton();

    }
    #endregion
}
