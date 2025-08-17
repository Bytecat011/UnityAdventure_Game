using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;

namespace Game.Gameplay.Features.AI.States
{
    public class AttackTriggerState : State, IUpdatableState
    {
        private ReactiveEvent _attaackRequest;

        public AttackTriggerState(Entity entity)
        {
            _attaackRequest = entity.StartAttackRequest;
        }

        public override void Enter()
        {
            base.Enter();
            
            _attaackRequest.Notify();
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}