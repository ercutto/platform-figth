
public abstract class PlayerBaseState 
{

    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;
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

    void UpdateStates() { }
    protected void SwitchState(PlayerBaseState newState) {
    // current states exit state
   
        ExitState();
       
        //new state to enters state
        newState.EnterState();
        _ctx.CurrentState = newState;


    }
    protected void SetSuperState() { }
    protected void SetSubState() { }
}
