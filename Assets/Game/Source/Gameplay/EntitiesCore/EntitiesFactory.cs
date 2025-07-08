using Game.Core.DI;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Gameplay.Features.ApplyDamage;
using Game.Gameplay.Features.LifeCycle;
using Game.Gameplay.Features.Movement;
using Game.Utility.Conditions;
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

        public Entity CreateGhost(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Ghost");
            
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddDamageEvent()
                .AddDamageRequest();

            var canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));
            
            var canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));
            
            var mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));
            
            var mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));
            
            var canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage);
            
            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesWorld));
            
            _entitiesWorld.Add(entity);
            
            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}