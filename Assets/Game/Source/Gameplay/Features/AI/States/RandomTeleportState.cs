using Game.Gameplay.EntitiesCore;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.Features.AI.States
{
    public class RandomTeleportState : State, IUpdatableState
    {
        private ReactiveEvent<Vector3> _useTeleportAbilityRequest;
        private ICondition _canUseTeleportAbility;
        private Transform _transform;
        private ReactiveVariable<float> _range;
        
        private float _cooldownBetweenTeleportation;

        private float _time;

        public RandomTeleportState(
            Entity entity,
            float cooldownBetweenTeleportation)
        {
            _cooldownBetweenTeleportation = cooldownBetweenTeleportation;
            
            _useTeleportAbilityRequest = entity.TeleportAbilityStartRequest;
            _canUseTeleportAbility = entity.CanUseTeleportAbility;
            _transform = entity.Transform;
            _range = entity.TeleportAbilityRange;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time >= _cooldownBetweenTeleportation)
            {
                if (_canUseTeleportAbility.Evaluate())
                {
                    UseTeleportAbility();
                    _time = 0f;
                }
            }
        }

        private void UseTeleportAbility()
        {
            var target = GetRandomXZPositionInRange(_transform.position, _range.Value);
            _useTeleportAbilityRequest.Notify(target);
        }
        
        private static Vector3 GetRandomXZPositionInRange(Vector3 center, float range)
        {
            var offset = Random.insideUnitSphere * range;
            offset.y = 0f;
            return center + offset;
        }
    }
}