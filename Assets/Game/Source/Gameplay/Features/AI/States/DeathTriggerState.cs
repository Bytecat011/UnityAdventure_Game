using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;

namespace Game.Gameplay.Features.AI.States
{
    public class DeathTriggerState : State, IUpdatableState
    {
        private ReactiveVariable<bool> _isDead;

        public DeathTriggerState(Entity entity)
        {
            _isDead = entity.IsDead;
        }

        public override void Enter()
        {
            base.Enter();
            
            _isDead.Value = true;
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}