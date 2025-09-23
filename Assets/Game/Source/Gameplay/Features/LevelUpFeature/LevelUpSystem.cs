using System;
using Game.Configs.Gameplay;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.LevelUpFeature
{
    public class LevelUpSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _level;
        private ExperienceForUpgradeLevelConfig _config;
        
        private IDisposable _expChangedSubscription;

        public LevelUpSystem(ExperienceForUpgradeLevelConfig config)
        {
            _config = config;
        }

        public float CurrentLimitForExp => _config.GetExperienceFor(_level.Value);
        
        public void OnInit(Entity entity)
        {
            _experience = entity.Experience;
            _level = entity.Level;

            _expChangedSubscription = _experience.Subscribe(OnExpChanged);
        }

        public void OnDispose()
        {
            _expChangedSubscription?.Dispose();
        }
        
        private void OnExpChanged(float oldExp, float newExp)
        {
            while (newExp >= CurrentLimitForExp && _level.Value < _config.MaxLevel)
            {
                newExp -= CurrentLimitForExp;
                _level.Value++;
            }
            
            _experience.Value = newExp;
        }
    }
}