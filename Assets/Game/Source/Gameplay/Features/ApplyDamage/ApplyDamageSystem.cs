using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Conditions;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.ApplyDamage
{
    public class ApplyDamageSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<float> _damageRequest;
        private ReactiveEvent<float> _damageEvent;

        private ReactiveVariable<float> _health;
        
        private ICompositeCondition _canApplyDamage;

        private IDisposable _requestSubscription;

        public void OnInit(Entity entity)
        {
            _damageRequest = entity.TakeDamageRequest;
            _damageEvent = entity.TakeDamageEvent;

            _health = entity.CurrentHealth;
            
            _canApplyDamage = entity.CanApplyDamage;

            _requestSubscription = _damageRequest.Subscribe(OnDamageRequest);
        }

        public void OnDispose()
        {
            _requestSubscription.Dispose();
        }

        private void OnDamageRequest(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));
            
            if (_canApplyDamage.Evaluate() == false)
                return;

            _health.Value = MathF.Max(_health.Value - damage, 0);
            
            _damageEvent.Notify(damage);
        }
    }
}