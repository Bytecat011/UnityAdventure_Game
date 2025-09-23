using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;

namespace Game.Gameplay.Features.Stats
{
    public class StatEffectsApplierSystem : IInitializableSystem, IDisposableSystem
    {
        private StatsEffectsList _statsEffects;
        private Dictionary<StatTypes, float> _baseStats;
        private Dictionary<StatTypes, float> _modifiedStats;
        
        public void OnInit(Entity entity)
        {
            _statsEffects = entity.StatsEffects;
            _baseStats = entity.BaseStats;
            _modifiedStats = entity.ModifiedStats;

            _statsEffects.Added += OnStatEffectAdded;
            _statsEffects.Removed += OnStatEffectRemoved;

            RecalculateStats();
        }

        public void OnDispose()
        {
            _statsEffects.Added -= OnStatEffectAdded;
            _statsEffects.Removed -= OnStatEffectRemoved;
        }

        private void RecalculateStats()
        {
            foreach (var (key, value) in _baseStats)
                _modifiedStats[key] = value;
            
            foreach (var effect in _statsEffects.Elements)
                effect.ApplyTo(_modifiedStats);
        }
        
        private void OnStatEffectAdded(IStatsEffect obj) => RecalculateStats();

        private void OnStatEffectRemoved(IStatsEffect obj) => RecalculateStats();
    }
}