
public abstract class PlayerBaseState 
{

    private bool _isRootState=false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSubState;
    private PlayerBaseState _currentSuperState;
    //Getter And Setters
    protected bool IsRootState { set { _isRootState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }   
    public PlayerBaseState(PlayerStateMachine currentContext,PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void  EnterState();
    public abstract void  UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubSttate();

    public void UpdateStates() { 
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }
    protected void SwitchState(PlayerBaseState newState) {
    // current states exit state
   
        ExitState();
       
        //new state to enters state
        newState.EnterState();

        if(_isRootState)
        {
            Ctx.CurrentState = newState;

        }else if(_currentSubState != null)
        {
            _currentSuperState.SetSuperState(newState);
        }
       


    }
    protected void SetSuperState(PlayerBaseState newSuperState) { _currentSuperState = newSuperState; }
    protected void SetSubState(PlayerBaseState newSubState) { _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
