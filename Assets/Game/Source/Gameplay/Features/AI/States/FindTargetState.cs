using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;

namespace Game.Gameplay.Features.AI.States
{
    public class FindTargetState : State, IUpdatableState
    {
        private ITargetSelector _targetSelector;
        private EntitiesWorld _entitiesWorld;
        private ReactiveVariable<Entity> _currentTarget;

        public FindTargetState(
            ITargetSelector targetSelector, 
            EntitiesWorld entitiesWorld,
            Entity entity)
        {
            _targetSelector = targetSelector;
            _entitiesWorld = entitiesWorld;
            _currentTarget = entity.CurrentTarget;
        }

        public void Update(float deltaTime)
        {
            _currentTarget.Value = _targetSelector.SelectTargetFrom(_entitiesWorld.Entities);
        }
    }
}