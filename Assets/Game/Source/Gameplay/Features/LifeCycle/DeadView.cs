using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.LifeCycle
{
    [RequireComponent(typeof(Animator))]
    public class DeadView : EntityView
    {
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");
        
        [SerializeField] private Animator _animator;
        
        private IReactiveVariable<bool> _isDead;
        
        private IDisposable _isDeadChangedSubscription;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isDead = entity.IsDead;

            _isDeadChangedSubscription = _isDead.Subscribe(OnIsDeadChanged);
            UpdateIsDead(_isDead.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _isDeadChangedSubscription?.Dispose();
        }

        private void UpdateIsDead(bool isDeadValue) => _animator.SetBool(IsDeadKey, isDeadValue);

        private void OnIsDeadChanged(bool _, bool isDead) => UpdateIsDead(isDead);
    }
}