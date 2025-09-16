using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    public class CurrentTargetView : EntityView
    {
        [SerializeField] private ParticleSystem _backlightPrefab;
        
        private ParticleSystem _backlight;
        
        private ReactiveVariable<Entity> _currentTarget;
        private Transform _currentTargetTransform;
        
        private IDisposable _currentTargetChangedSubscription;
        
        protected override void OnEntityStartedWork(Entity entity)
        {
            _currentTarget = entity.CurrentTarget;

            _backlight = Instantiate(_backlightPrefab);

            _currentTargetChangedSubscription = _currentTarget.Subscribe(OnCurrentTargetChanged);
            
            UpdateBacklightFor(_currentTarget.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _currentTargetChangedSubscription?.Dispose();
            Destroy(_backlight.gameObject);
        }

        private void LateUpdate()
        {
            if (_currentTargetTransform == null)
                return;
            
            _backlight.transform.position = _currentTargetTransform.position;
        }

        private void UpdateBacklightFor(Entity currentTargetValue)
        {
            if (currentTargetValue == null)
            {
                _backlight.gameObject.SetActive(false);
                _currentTargetTransform = null;
                return;
            }
            
            _backlight.gameObject.SetActive(true);
            _currentTargetTransform = currentTargetValue.Transform;
        }

        private void OnCurrentTargetChanged(Entity _, Entity newTargetEntity)
        {
            UpdateBacklightFor(newTargetEntity);
        }
    }
}