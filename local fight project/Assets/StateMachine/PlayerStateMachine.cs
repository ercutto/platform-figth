using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    CharacterController _characterController;
    Animator _animator;
    inputs _input;
    int _isWalkingHash;
    int _isRunningHash;
    Vector2 _currentMovementInput;
    Vector2 _currentMovement;
    Vector3 _apliedMovement;
    bool _isRunPressed;
    bool _isMovementPressed;
    float _rotationFactorPerFrame=15.0f;
    float _runMultiplier=4.0f;
    int _zero = 0;

    float _gravity = -9.8f;
    float _grondedGravity = -.05f;

    bool _isJumpPressed=false;
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    float _initialJumpVelocity;
    float _maxJumpHeight = 4.0f;
    float _maxJumpTime = .75f;
    bool _isJumping=false;
    int _isJumpingHash;
    int _isJumpCountHash;
    bool _isJumpAnimating;
    int _jumpCount;

    //stateVariable
    PlayerBaseState _currentState;
    public PlayerBaseState CurrentState { get { return _currentState; }set { _currentState = value; } }
    PlayerStateFactory _states;
    Dictionary<int,float>_initialJumpVelocities = new Dictionary<int,float>();
    Dictionary<int,float> _jumpGravities=new Dictionary<int,float>();
    Coroutine _currentJumpResetCoroutine = null;

    void Awake()
    {
        _input = new inputs();
        _characterController=GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _states= new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isJumpCountHash = Animator.StringToHash("jumpcount");

       
        _input.PlayerInputs.move.started += OnMovementInput;
        _input.PlayerInputs.move.canceled += OnMovementInput;
        _input.PlayerInputs.move.performed += OnMovementInput;
        _input.PlayerInputs.Jump.started += OnJump;
        _input.PlayerInputs.Jump.canceled += OnJump;
        _input.PlayerInputs.Run.started += OnRunPressed;
        _input.PlayerInputs.Run.canceled += OnRunPressed;

        SetupJumpVariables();
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        
        HandleAnimation();
        _characterController.Move(_apliedMovement*Time.deltaTime);
    }

    private void HandleAnimation()
    {
       
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = _currentMovementInput.x;
        positionToLookAt.y=_zero;
        positionToLookAt.z = _currentMovementInput.y;
        Quaternion currentRotation=transform.rotation;
        if(_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

            transform.rotation = Quaternion.Slerp(currentRotation,targetRotation,_rotationFactorPerFrame*Time.deltaTime);
        }
    }

    void SetupJumpVariables()
    {
        float timetoApex = _maxJumpTime / 2;
        _gravity=(-2*_maxJumpHeight)/Mathf.Pow(timetoApex,2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timetoApex;
        float secondJumpgravity=(-2*(_maxJumpHeight+2))/Mathf.Pow((timetoApex*1.25f),2);
        float secondJumpInitialVelocity=(2*(_maxJumpHeight+2))/(timetoApex*1.25f);
        float thirdJumpgravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timetoApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 4)) / (timetoApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        _jumpGravities.Add(0, _gravity);
        _jumpGravities.Add(1, _gravity);
        _jumpGravities.Add(2, secondJumpgravity);
        _jumpGravities.Add(0, thirdJumpgravity);
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed=_currentMovementInput.x!=_zero||_currentMovementInput.y!=_zero;
    }
    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed=context.ReadValueAsButton();

    }
    void OnRunPressed(InputAction.CallbackContext context)
    {
        _isRunPressed=context.ReadValueAsButton();
       
    }
    void OnEnable()
    {
        _input.Enable();
        
    }
    void OnDisable()
    {
        _input.Disable();
    }
}
