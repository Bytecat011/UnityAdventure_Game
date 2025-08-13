using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    public class EndAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _endAttackEvent;
        private ReactiveVariable<bool> _inAttackProcess;
        private ReactiveVariable<float> _attackProcessInitialTime;
        private ReactiveVariable<float> _attackProcessCurrentTime;

        private IDisposable _timerSubscription;
        
        public void OnInit(Entity entity)
        {
            _endAttackEvent = entity.EndAttackEvent;
            _inAttackProcess = entity.InAttackProcess;
            _attackProcessInitialTime = entity.AttackProcessInitialTime;
            _attackProcessCurrentTime = entity.AttackProcessCurrentTime;

            _timerSubscription = _attackProcessCurrentTime.Subscribe(OnTimerChanged);
        }

        private void OnTimerChanged(float _, float currentTime)
        {
            if (TimeIsDone(currentTime))
            {
                Debug.Log("Attack end");
                _inAttackProcess.Value = false;
                _endAttackEvent.Notify();
            }
        }

        private bool TimeIsDone(float currentTime) => currentTime >= _attackProcessInitialTime.Value;

        public void OnDispose()
        {
            _timerSubscription.Dispose();
        }
    }
}