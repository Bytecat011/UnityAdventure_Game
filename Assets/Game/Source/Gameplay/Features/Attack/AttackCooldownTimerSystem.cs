using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    public class AttackCooldownTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<bool> _inAttackCooldown;
        private ReactiveEvent _endAttackEvent;

        private ISubscription _endAttackEventSubscription;
        
        public void OnInit(Entity entity)
        {
            _currentTime = entity.AttackCooldownCurrentTime;
            _initialTime = entity.AttackCooldownInitialTime;
            _inAttackCooldown = entity.InAttackCooldown;
            _endAttackEvent = entity.EndAttackEvent;

            _endAttackEventSubscription = _endAttackEvent.Subscribe(OnEndAttack);
        }

        private void OnEndAttack()
        {
            Debug.Log("Attack cooldown started");
            _currentTime.Value = _initialTime.Value;
            _inAttackCooldown.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAttackCooldown.Value == false)
                return;
            
            _currentTime.Value -= deltaTime;

            if (CooldownIsOver())
            {
                _inAttackCooldown.Value = false;
                Debug.Log("Cooldown over");
            }
        }

        private bool CooldownIsOver() => _currentTime.Value <= 0;

        public void OnDispose()
        {
            _endAttackEventSubscription.Unsubscribe();
        }
    }
}