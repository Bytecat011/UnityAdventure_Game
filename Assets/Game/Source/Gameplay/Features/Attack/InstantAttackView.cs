using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    [RequireComponent(typeof(Animator))]
    public class InstantAttackView : EntityView
    {
        private readonly int IsAttackKey = Animator.StringToHash("IsAttack");
        
        [SerializeField] private Animator _animator;
        
        private IReactiveVariable<bool> _inAttackProcess;
        
        private IDisposable _isAttackChangedSubscription;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _inAttackProcess = entity.InAttackProcess;

            _isAttackChangedSubscription = _inAttackProcess.Subscribe(OnAttackProcessChanged);
            UpdateIsAttack(_inAttackProcess.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _isAttackChangedSubscription?.Dispose();
        }

        private void UpdateIsAttack(bool isAttackValue) => _animator.SetBool(IsAttackKey, isAttackValue);

        private void OnAttackProcessChanged(bool _, bool isAttack) => UpdateIsAttack(isAttack);
    }
}