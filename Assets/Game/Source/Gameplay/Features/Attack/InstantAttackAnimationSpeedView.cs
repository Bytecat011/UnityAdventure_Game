using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    public class InstantAttackAnimationSpeedView : EntityView
    {
        private readonly int _attackAnimationSpeedMultiplierKey =
            Animator.StringToHash("AttackAnimationSpeedMultiplier");

        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private Animator _animator;

        private IReactiveVariable<float> _attackProcessTime;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _attackProcessTime = entity.AttackProcessInitialTime;
            
            _animator.SetFloat(_attackAnimationSpeedMultiplierKey, _animationClip.length / _attackProcessTime.Value);
        }
    }
}