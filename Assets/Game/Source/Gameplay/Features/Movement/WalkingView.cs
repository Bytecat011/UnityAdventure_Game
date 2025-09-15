using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Movement
{
    [RequireComponent(typeof(Animator))]
    public class WalkingView : EntityView
    {
        private readonly int IsMovingKey = Animator.StringToHash("IsMoving");
        
        [SerializeField] private Animator _animator;
        
        private IReactiveVariable<bool> _isMoving;
        
        private IDisposable _isMovingChangedSubscription;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isMoving = entity.IsMoving;

            _isMovingChangedSubscription = _isMoving.Subscribe(OnIsMovingChanged);
            UpdateIsMoving(_isMoving.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _isMovingChangedSubscription?.Dispose();
        }

        private void UpdateIsMoving(bool isMovingValue) => _animator.SetBool(IsMovingKey, isMovingValue);

        private void OnIsMovingChanged(bool _, bool isMoving) => UpdateIsMoving(isMoving);
    }
}