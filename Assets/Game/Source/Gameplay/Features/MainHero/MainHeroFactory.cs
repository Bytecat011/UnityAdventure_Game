using Game.Configs.Gameplay.Entities;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.AI.States;
using Game.Utility.Configs;
using UnityEngine;

namespace Game.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ConfigManager _configManager;
        private readonly EntitiesWorld _entitiesWorld;
        
        public MainHeroFactory(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _configManager = _container.Resolve<ConfigManager>();
            _entitiesWorld = _container.Resolve<EntitiesWorld>();
        }

        public Entity Create(Vector3 position)
        {
            HeroConfig config = _configManager.GetConfig<HeroConfig>();
            
            Entity entity = _entitiesFactory.CreateHero(position, config);

            entity.AddIsMainHero();
            
            entity.AddCurrentTarget();
            _brainsFactory.CreateMainHeroBrain(entity, new NearestDamageableTargetSelector(entity));

            _entitiesWorld.Add(entity);
            
            return entity;
        }
    }
}