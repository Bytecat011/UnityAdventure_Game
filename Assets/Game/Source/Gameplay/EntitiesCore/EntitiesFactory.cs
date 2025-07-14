using Game.Core.DI;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Gameplay.Features.ApplyDamage;
using Game.Gameplay.Features.Attack;
using Game.Gameplay.Features.Attack.AreaDamage;
using Game.Gameplay.Features.Attack.Shoot;
using Game.Gameplay.Features.ContactTakeDamage;
using Game.Gameplay.Features.Energy;
using Game.Gameplay.Features.LifeCycle;
using Game.Gameplay.Features.Movement;
using Game.Gameplay.Features.Sensors;
using Game.Gameplay.Features.TeleportAbility;
using Game.Utility;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesWorld _entitiesWorld;
        private readonly CollidersRegistryService _collidersRegistryService;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesWorld = container.Resolve<EntitiesWorld>();
            _monoEntitiesFactory = container.Resolve<MonoEntitiesFactory>();
            _collidersRegistryService = container.Resolve<CollidersRegistryService>();
        }

        public Entity CreateHero(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Hero");

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageEvent()
                .AddTakeDamageRequest()
                .AddAttackProcessInitialTime(new ReactiveVariable<float>(3))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddStartAttackRequest()
                .AddStartAttackEvent()
                .AddEndAttackEvent()
                .AddAttackDelayTime(new ReactiveVariable<float>(1))
                .AddAttackDelayEndEvent()
                .AddInstantAttackDamage(new ReactiveVariable<float>(50))
                .AddAttackCancelEvent()
                .AddAttackCooldownInitialTime(new ReactiveVariable<float>(2))
                .AddAttackCooldownCurrentTime()
                .AddInAttackCooldown();

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

            var canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
                .Add(new FuncCondition(() => entity.IsMoving.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false));
            
            var mustCancelAttack = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.IsMoving.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack)
                .AddMustCancelAttack(mustCancelAttack);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new AttackCancelSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessTimerSystem())
                .AddSystem(new AttackDelayEndTriggerSystem())        
                .AddSystem(new InstantShootSystem(this))
                .AddSystem(new EndAttackSystem())
                .AddSystem(new AttackCooldownTimerSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesWorld));

            _entitiesWorld.Add(entity);

            return entity;
        }
        
        public Entity CreateGhost(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Ghost");

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageEvent()
                .AddTakeDamageRequest()
                .AddContactsDetectingMask(UnityLayers.LayerMaskCharacters)
                .AddContactColliderBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(50));

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
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesWorld));

            _entitiesWorld.Add(entity);

            return entity;
        }
        
        public Entity CreateProjectile(Vector3 position, Vector3 direction, float damage)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Projectile");

            entity
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection(new ReactiveVariable<Vector3>(direction))
                .AddRotationSpeed(new ReactiveVariable<float>(9999))
                .AddIsDead()
                .AddContactsDetectingMask(UnityLayers.LayerMaskCharacters)
                .AddContactColliderBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(damage))
                .AddDeathMask(UnityLayers.LayerMaskCharacters)
                .AddIsTouchDeathMask();

            var canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            var canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            var mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsTouchDeathMask.Value));

            var mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new DeathMaskTouchDetectorSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesWorld));

            _entitiesWorld.Add(entity);

            return entity;
        }

        public Entity CreateTeleportingCharacter(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/TeleportingCharacter");

            entity
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageEvent()
                .AddTakeDamageRequest()
                .AddMaximumEnergy(new ReactiveVariable<float>(80))
                .AddCurrentEnergy(new ReactiveVariable<float>(80))
                .AddEnergyRestorationInterval(new ReactiveVariable<float>(2))
                .AddEnergyRestorationPercentageAmount(new ReactiveVariable<float>(10))
                .AddEnergyRestorationTimer()
                .AddTeleportAbilityCastCurrentTime()
                .AddTeleportAbilityCastInitialTime(new ReactiveVariable<float>(0))
                .AddTeleportAbilityStartRequest()
                .AddTeleportAbilityStartEvent()
                .AddTeleportAbilityEnergyCost(new ReactiveVariable<float>(15))
                .AddTeleportAbilityRange(new ReactiveVariable<float>(5f))
                .AddInTeleportAbilityCastProcess()
                .AddTeleportAbilityEndEvent()
                .AddAreaDamage(new ReactiveVariable<float>(50))
                .AddAreaDamageRange(new ReactiveVariable<float>(3f))
                .AddAreaDamageRequest()
                .AddAreaDamageLayerMask(UnityLayers.LayerMaskCharacters);

            var mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            var mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            var canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));
            
            var canUseTeleportAbility = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.CurrentEnergy.Value >= entity.TeleportAbilityEnergyCost.Value))
                .Add(new FuncCondition(() => entity.InTeleportAbilityCastProcess.Value == false));

            entity
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanUseTeleportAbility(canUseTeleportAbility);

            entity
                .AddSystem(new EnergyRestorationSystem())
                .AddSystem(new StartTeleportAbilitySystem())
                .AddSystem(new TeleportAbilityCastSystem())
                .AddSystem(new TeleportAbilitySystem())
                .AddSystem(new AreaDamageOnTeleportSystem())
                .AddSystem(new InstantAreaDamageSystem(_collidersRegistryService))
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesWorld));

            _entitiesWorld.Add(entity);

            return entity;
        }
        
        private Entity CreateEmpty() => new Entity();
    }
}