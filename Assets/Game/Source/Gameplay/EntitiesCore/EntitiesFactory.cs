using Game.Core.DI;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Gameplay.Features.Movement;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesWorld _entitiesWorld;

        private readonly MonoEntitiesFactory _monoEntitiesFactory;
        
        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesWorld = container.Resolve<EntitiesWorld>();
            _monoEntitiesFactory = container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateTestEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/TestEntity");
            
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10));

            entity.AddSystem(new RigidbodyMovementSystem());
            
            _entitiesWorld.Add(entity);
            
            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}