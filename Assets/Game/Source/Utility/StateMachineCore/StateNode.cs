using System.Collections.Generic;

namespace Game.Utility.StateMachineCore
{
    public class StateNode<TState> where TState : class, IState
    {
        private List<StateTransition<TState>> _transitions = new();
        
        public StateNode(TState state, params StateTransition<TState>[] transitions)
        {
            State = state;
            _transitions.AddRange(transitions);
        }

        public TState State { get; }
        
        public IReadOnlyList<StateTransition<TState>> Transitions => _transitions;
        
        public void AddTransition(StateTransition<TState> transition) => _transitions.Add(transition);
    }
}