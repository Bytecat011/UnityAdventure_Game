using System;

namespace Game.Gameplay.Features.AI
{
    public class StateMachineBrain : IBrain
    {
        private AIStateMachine _stateMachine;

        private bool _isEnabled;

        public StateMachineBrain(AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enable()
        {
            _stateMachine.Enter();
            _isEnabled = true;   
        }

        public void Disable()
        {
            _stateMachine.Exit();
            _isEnabled = false;
        }

        public void Update(float deltaTime)
        {
            if (_isEnabled == false)
                return;
            
            _stateMachine.Update(deltaTime);
        }

        public void Dispose()
        {
            _stateMachine.Dispose();
            _isEnabled = false;
        }
    }
}