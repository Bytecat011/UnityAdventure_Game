using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack.TowerAttack
{
    public class TowerAttackSystem: IInitializableSystem, IDisposableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;

        private Entity _entity;
        
        private ReactiveEvent _attackDelayEndEvent;
        private ReactiveVariable<float> _damage;

        private IDisposable _attackDelayEndSubscription;

        public TowerAttackSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            
            _attackDelayEndEvent = entity.AttackDelayEndEvent;
            _damage = entity.TowerAttackDamage;

            _attackDelayEndSubscription = _attackDelayEndEvent.Subscribe(OnAttackDelayEnd);
        }

        private void OnAttackDelayEnd()
        {
            
        }

        public void OnDispose()
        {
            _attackDelayEndSubscription.Dispose();
        }
    }
}