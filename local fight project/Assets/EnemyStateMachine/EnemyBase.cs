public class EnemyBase 
{
    public string name;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyBase(string name,EnemyStateMachine enemyStateMachine)
    {
        this.name = name;
        this.enemyStateMachine = enemyStateMachine; 
    }
    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void Exit() { }

}
