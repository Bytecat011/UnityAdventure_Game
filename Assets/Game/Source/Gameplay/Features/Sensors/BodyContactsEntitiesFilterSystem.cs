using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility;
using UnityEngine;

namespace Game.Gameplay.Features.Sensors
{
    public class BodyContactsEntitiesFilterSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly CollidersRegistryService _collidersRegistryService;
        
        private Buffer<Collider> _contacts;
        private Buffer<Entity> _contactsEntities;

        public BodyContactsEntitiesFilterSystem(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactColliderBuffer;
            _contactsEntities = entity.ContactEntitiesBuffer;
        }

        public void OnUpdate(float deltaTime)
        {
            _contactsEntities.Count = 0;

            for (int i = 0; i < _contacts.Count; i++)
            {
                var collider = _contacts.Items[i];

                var contactEntity = _collidersRegistryService.GetBy(collider);

                if (contactEntity != null)
                    _contactsEntities.Add(contactEntity);
            }
        }
    }
}