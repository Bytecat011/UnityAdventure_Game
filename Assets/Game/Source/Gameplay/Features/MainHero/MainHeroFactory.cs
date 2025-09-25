using Game.Configs.Gameplay;
using Game.Configs.Gameplay.Entities;
using Game.Core.DI;
using Game.Gameplay.Core;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Abilities;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.AI.States;
using Game.Gameplay.Features.LevelUpFeature;
using Game.Gameplay.Features.TeamsFeatures;
using Game.Utility.Configs;
using Game.Utility.Reactive;
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
        private readonly GameplayInputArgs _inputArgs;
        
        public MainHeroFactory(DIContainer container, GameplayInputArgs inputArgs)
        {
            _container = container;
            _inputArgs = inputArgs;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _configManager = _container.Resolve<ConfigManager>();
            _entitiesWorld = _container.Resolve<EntitiesWorld>();
        }

        public Entity Create(Vector3 position)
        {
            PlayerTowerConfig config = _configManager.GetConfig<PlayerTowerConfig>();
            
            Entity entity = _entitiesFactory.CreatePlayerTower(position, config, _inputArgs.LevelConfig);

            entity
                .AddIsMainHero()
                .AddTeam(new ReactiveVariable<Teams>(Teams.MainHero));

            entity
                .AddAbilities()
                .AddSystem(new AbilityOnAddActivatorSystem());

            _entitiesWorld.Add(entity);
            
            return entity;
        }
    }
}