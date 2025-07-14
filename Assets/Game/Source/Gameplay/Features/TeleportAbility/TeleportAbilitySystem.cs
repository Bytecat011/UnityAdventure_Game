using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.TeleportAbility
{
    public class TeleportAbilitySystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveEvent _endEvent;
        private Rigidbody _rigidbody;
        private ReactiveVariable<float> _currentEnergy;
        private ReactiveVariable<float> _energyCost;
        private ReactiveVariable<float> _range;
        private ReactiveVariable<float> _timer;
        private ReactiveVariable<bool> _inCastProcess;
        
        public void OnInit(Entity entity)
        {
            _endEvent = entity.TeleportAbilityEndEvent;
            _rigidbody = entity.Rigidbody;
            _currentEnergy = entity.CurrentEnergy;
            _energyCost = entity.TeleportAbilityEnergyCost;
            _range = entity.TeleportAbilityRange;
            _timer = entity.TeleportAbilityCastCurrentTime;
            _inCastProcess = entity.InTeleportAbilityCastProcess;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inCastProcess.Value == false)
                return;

            if (_timer.Value <= 0)
            {
                _inCastProcess.Value = false;
                if (CanUseAbility())
                {
                    PerformAbility();
                }
            }
        }

        private void PerformAbility()
        {
            _currentEnergy.Value -= _energyCost.Value;

            var currentPosition = _rigidbody.position;
            var offset = Random.insideUnitSphere * _range.Value;
            offset.y = 0f;
            var newPosition = currentPosition + offset;
            
            _rigidbody.MovePosition(newPosition);
            
            _endEvent.Notify();
        }

        private bool CanUseAbility() => _currentEnergy.Value >= _energyCost.Value;
    }
}