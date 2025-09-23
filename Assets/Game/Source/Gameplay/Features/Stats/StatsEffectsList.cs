using System;
using System.Collections.Generic;

namespace Game.Gameplay.Features.Stats
{
    public class StatsEffectsList
    {
        public event Action<IStatsEffect> Added;
        public event Action<IStatsEffect> Removed;
        
        private List<IStatsEffect> _effects = new();
        
        public IReadOnlyList<IStatsEffect> Elements => _effects;

        public virtual void Add(IStatsEffect effect)
        {
            _effects.Add(effect);
            Added?.Invoke(effect);
        }

        public virtual void Remove(IStatsEffect effect)
        {
            _effects.Remove(effect);
            Removed?.Invoke(effect);
        }
    }
}