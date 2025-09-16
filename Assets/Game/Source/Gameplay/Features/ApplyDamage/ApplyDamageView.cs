using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.ApplyDamage
{
    public class ApplyDamageView : EntityView
    {
        [SerializeField] private ParticleSystem _applyDamageEffectPrefab;
        [SerializeField] private Transform _effectSpawnPoint;

        private ReactiveEvent<float> _damageEvent;
        
        private IDisposable _damageEventSubscription;
        
        protected override void OnEntityStartedWork(Entity entity)
        {
            _damageEvent = entity.TakeDamageEvent;
            _damageEventSubscription = _damageEvent.Subscribe(OnDamaged);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _damageEventSubscription?.Dispose();
        }

        private void OnDamaged(float _)
        {
            Instantiate(_applyDamageEffectPrefab, _effectSpawnPoint.position, Quaternion.identity);
        }
    }
}