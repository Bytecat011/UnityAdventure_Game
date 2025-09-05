using System;
using Game.Configs.Gameplay.Entities;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.TeamsFeatures;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Enemies
{
    public class EnemiesFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesWorld _entitiesWorld;
        
        public EnemiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _entitiesWorld = _container.Resolve<EntitiesWorld>();
        }

        public Entity Create(Vector3 position, EntityConfig config)
        {
           Entity entity;

           switch (config)
           {
               case GhostConfig gohostConfig:
                   entity = _entitiesFactory.CreateGhost(position, gohostConfig);
                   _brainsFactory.CreateGhostBrain(entity);
                   break;
               default:
                   throw new ArgumentException($"Not supported {config.GetType()} type config");
           }
           
           entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemies));
           
           _entitiesWorld.Add(entity);
           
            return entity;
        }
    }
}