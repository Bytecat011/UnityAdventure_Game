using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Energy
{
    public class EnergyRestorationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _currentEnergy;
        private ReactiveVariable<float> _maxEnergy;
        private ReactiveVariable<float> _restorationInterval;
        private ReactiveVariable<float> _restorationPercentage;
        private ReactiveVariable<float> _timer;
        
        public void OnInit(Entity entity)
        {
            _maxEnergy = entity.MaximumEnergy;
            _currentEnergy = entity.CurrentEnergy;
            _restorationInterval = entity.EnergyRestorationInterval;
            _restorationPercentage = entity.EnergyRestorationPercentageAmount;
            _timer = entity.EnergyRestorationTimer;
        }

        public void OnUpdate(float deltaTime)
        {
            _timer.Value += deltaTime;
            if (_timer.Value >= _restorationInterval.Value)
            {
                _timer.Value = 0;
                RestoreEnergy();
            }
        }

        private void RestoreEnergy()
        {
            _currentEnergy.Value = Mathf.Clamp(_currentEnergy.Value + GetRestorationAmount(), 0f, _maxEnergy.Value);
            Debug.Log($"Energy restored. Current energy: {_currentEnergy.Value}/{_maxEnergy.Value}");
        }

        private float GetRestorationAmount() => _maxEnergy.Value * _restorationPercentage.Value * 0.01f;
    }
}