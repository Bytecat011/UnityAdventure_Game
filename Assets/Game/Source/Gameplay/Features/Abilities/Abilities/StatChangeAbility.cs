using Game.Configs.Gameplay.Abilities;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Stats;

namespace Game.Gameplay.Features.Abilities.Abilities
{
    public class StatChangeAbility : Ability
    {
        private Entity _entity;
        private StatChangeAbilityConfig _config;

        public StatChangeAbility(Entity entity, StatChangeAbilityConfig config) : base(config.ID)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            _entity.StatsEffects.Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}