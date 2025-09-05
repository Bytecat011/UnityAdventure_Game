using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Gameplay.Features.ApplyDamage;
using Game.Gameplay.Features.TeamsFeatures;
using Game.Utility;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.ContactTakeDamage
{
    public class DealDamageOnContactSystem : IInitializableSystem, IUpdatableSystem
    {
        private Entity _entity;
        private Buffer<Entity> _contacts;
        private ReactiveVariable<float> _damage;

        private List<Entity> _processedEntities;
        
        public void OnInit(Entity entity)
        {
            _entity = entity;
            
            _contacts = entity.ContactEntitiesBuffer;
            _damage = entity.BodyContactDamage;
            
            _processedEntities = new List<Entity>(_contacts.Items.Length);
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                var contactEntity = _contacts.Items[i];
                
                if (_processedEntities.Contains(contactEntity) == false)
                {
                    _processedEntities.Add(contactEntity);

                    EntitiesHelper.TryTakeDamageFrom(_entity, contactEntity, _damage.Value);
                }
            }

            for (int i = _processedEntities.Count - 1; i >= 0; i--)
                if (_contacts.Contains(_processedEntities[i]) == false)
                    _processedEntities.RemoveAt(i);
        }
    }
}