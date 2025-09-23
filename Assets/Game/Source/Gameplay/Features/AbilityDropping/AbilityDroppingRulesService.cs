using Game.Configs.Gameplay.Abilities;
using Game.Gameplay.EntitiesCore;

namespace Game.Gameplay.Features.AbilityDropping
{
    public class AbilityDroppingRulesService
    {
        public bool IsAvailable(AbilityConfig config, Entity entity)
        {
            switch (config)
            {
                case StatChangeAbilityConfig statChangeConfig:
                    return entity.TryGetModifiedStats(out var modifiedStats)
                        && modifiedStats.ContainsKey(statChangeConfig.StatType);
            }

            return true;
        }
    }
}