using System;
using System.Collections.Generic;
using Game.Utility.StateMachineCore;

namespace Game.Gameplay.Features.AI
{
    public class AIStateMachine : StateMachine<IUpdatableState>
    {
        public AIStateMachine(List<IDisposable> disposables) : base(disposables)
        {
        }

        public AIStateMachine() : base(new List<IDisposable>())
        {
            
        }

        protected override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);
            
            CurrentState?.Update(deltaTime);
        }
    }
}