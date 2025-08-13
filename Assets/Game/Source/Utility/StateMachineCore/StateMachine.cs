using System;
using System.Collections.Generic;
using System.Linq;
using Game.Utility.Conditions;

namespace Game.Utility.StateMachineCore
{
    public abstract class StateMachine<TState> : IDisposable where TState : class, IState
    {
        private List<StateNode<TState>> _states = new();
        
        private StateNode<TState> _currentState;

        private bool _isRunning;
        
        protected TState CurrentState => _currentState?.State;
        
        public void AddState(TState state) => _states.Add(new StateNode<TState>(state));

        public void AddTransition(TState fromState, TState toState, ICondition condition)
        {
            var from = _states.First(stateNode => stateNode.State == fromState);
            var to = _states.First(stateNode => stateNode.State == toState);
            
            from.AddTransition(new StateTransition<TState>(to, condition));
        }

        public void Update(float deltaTime)
        {
            if(_isRunning == false)
                return;

            foreach (var transition in _currentState.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    SwitchState(transition.ToState);
                    break;
                }
            }
        }

        public void Enter()
        {
            if (_currentState == null)
                SwitchState(_states[0]);
            
            _isRunning = true;
        }

        public void Exit()
        {
            _currentState?.State.Exit();
            
            _isRunning = false;
        }
        
        public void Dispose()
        {
            _isRunning = false;
            
            foreach (var stateNode in _states)
                if (stateNode.State is IDisposable disposableState)
                    disposableState.Dispose();
            
            _states.Clear();
        }

        private void SwitchState(StateNode<TState> nextState)
        {
            _currentState?.State.Exit();
            _currentState = nextState;
            _currentState.State.Enter();
        }
    }
}