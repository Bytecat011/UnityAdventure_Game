using Game.Gameplay.EntitiesCore;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.Features.AI.States
{
    public class TeleportToTargetState : State, IUpdatableState
    {
        private ReactiveEvent<Vector3> _useTeleportAbilityRequest;
        private ReactiveVariable<Entity> _currentTarget;
        private ICondition _canUseTeleportAbility;
        
        private float _cooldownBetweenTeleportation;
        
        private float _time;
        
        public TeleportToTargetState(
            Entity entity,
            float cooldownBetweenTeleportation)
        {
            _cooldownBetweenTeleportation = cooldownBetweenTeleportation;
            
            _useTeleportAbilityRequest = entity.TeleportAbilityStartRequest;
            _canUseTeleportAbility = entity.CanUseTeleportAbility;
            _currentTarget = entity.CurrentTarget;
        }
        
        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time >= _cooldownBetweenTeleportation)
            {
                if (_currentTarget.Value != null && _canUseTeleportAbility.Evaluate())
                {
                    _useTeleportAbilityRequest.Notify(_currentTarget.Value.Transform.position);
                    _time = 0f;
                }
            }
        }
    }
}