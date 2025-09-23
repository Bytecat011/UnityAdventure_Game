using System;
using Game.Configs.Gameplay.Abilities;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Abilities.Abilities;

namespace Game.Gameplay.Features.Abilities
{
    public class AbilityFactory
    {
        private DIContainer _container;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
        }

        public Ability CreateAbilityFor(Entity entity, AbilityConfig config)
        {
            switch (config)
            {
                case StatChangeAbilityConfig changeStatConfig:
                    return new StatChangeAbility(entity, changeStatConfig);
                
                default:
                    throw new ArgumentException();
            }
        }
    }
}