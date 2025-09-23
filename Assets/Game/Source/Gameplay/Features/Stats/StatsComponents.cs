using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;

namespace Game.Gameplay.Features.Stats
{
    public class BaseStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }

    public class ModifiedStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }

    public class StatsEffects : IEntityComponent
    {
        public StatsEffectsList Value;
    }
}