using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    public class AttackDelayEndTriggerSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _attackDelayEndEvent;
        private ReactiveVariable<float> _delay;
        private ReactiveVariable<float> _attackProcessCurrentTime;
        private ReactiveEvent _startAttackEvent;
        
        private bool _alreadyAttacked;
        
        private ISubscription _timerSubscription;
        private ISubscription _startAttackSubscription;

        public void OnInit(Entity entity)
        {
            _attackDelayEndEvent = entity.AttackDelayEndEvent;
            _delay = entity.AttackDelayTime;
            _attackProcessCurrentTime = entity.AttackProcessCurrentTime;
            _startAttackEvent = entity.StartAttackEvent;

            _timerSubscription = _attackProcessCurrentTime.Subscribe(OnTimerChanged);
            _startAttackSubscription = _startAttackEvent.Subscribe(OnStartAttack);
        }

        private void OnStartAttack()
        {
            _alreadyAttacked = false;
        }

        private void OnTimerChanged(float _, float currentTime)
        {
            if (_alreadyAttacked)
                return;
            
            if (currentTime >= _delay.Value)
            {
                Debug.Log("Attack delay ended");
                _attackDelayEndEvent.Notify();
                _alreadyAttacked = true;
            }
        }

        public void OnDispose()
        {
            _timerSubscription.Unsubscribe();
            _startAttackSubscription.Unsubscribe();
        }
    }
}