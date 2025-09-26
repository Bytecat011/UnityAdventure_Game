using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.Attack.TowerAttack
{
    public class InstantExplosionAttackSystem: IInitializableSystem, IDisposableSystem
    {
        private Entity _entity;
        
        private ReactiveEvent _attackEndEvent;
        private Buffer<Entity> _contacts;
        private ReactiveVariable<float> _damage;

        private IDisposable _attackDelayEndSubscription;

        public void OnInit(Entity entity)
        {
            _entity = entity;
            
            _attackEndEvent = entity.EndAttackEvent;
            _contacts = entity.ContactEntitiesBuffer;
            _damage = entity.InstantAttackDamage;

            _attackDelayEndSubscription = _attackEndEvent.Subscribe(OnAttackEnd);
        }

        private void OnAttackEnd()
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                var contactEntity = _contacts.Items[i];
                
                EntitiesHelper.TryTakeDamageFrom(_entity, contactEntity, _damage.Value);
            }
        }

        public void OnDispose()
        {
            _attackDelayEndSubscription.Dispose();
        }
    }
}