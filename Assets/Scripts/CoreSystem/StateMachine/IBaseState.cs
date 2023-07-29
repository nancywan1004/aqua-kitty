namespace CoreSystem.StateMachine
{
    public interface IBaseState
    {
        void OnStateEnter(BaseStateMachine sm);
        void OnStateExit(BaseStateMachine sm);
        void UpdateState(BaseStateMachine sm);
    }
}