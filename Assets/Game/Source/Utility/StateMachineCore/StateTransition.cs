using Game.Utility.Conditions;

namespace Game.Utility.StateMachineCore
{
    public class StateTransition<TState> where TState : class, IState
    {
        public StateTransition(StateNode<TState> toState, ICondition condition)
        {
            ToState = toState;
            Condition = condition;
        }

        public StateNode<TState> ToState { get; }
        public ICondition Condition { get; }
    }
}