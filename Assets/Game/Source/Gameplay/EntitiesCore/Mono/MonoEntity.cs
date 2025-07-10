using UnityEngine;

namespace Game.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        private CollidersRegistryService _collidersRegistryService;
        
        private Entity _linkedEntity;
        
        public Entity LinkedEntity => _linkedEntity;

        public void Initialize(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }
        
        public void Link(Entity entity)
        {
            _linkedEntity = entity;
            
            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();
            
            if(registrators != null)
                foreach(MonoEntityRegistrator registrator in registrators)
                    registrator.Register(entity);

            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Register(collider, entity);
        }

        public void Cleanup(Entity entity)
        {
            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Unregister(collider);
            
            _linkedEntity = null;
        }
    }
}