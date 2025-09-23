using System.Collections.Generic;

namespace Game.Gameplay.Features.Stats
{
    public interface IStatsEffect
    {
        void ApplyTo(Dictionary<StatTypes, float> stats);
    }
}