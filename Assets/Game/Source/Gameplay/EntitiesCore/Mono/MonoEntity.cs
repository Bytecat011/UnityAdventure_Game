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

            var views = GetComponentsInChildren<EntityView>();
            if (views != null)
                foreach(EntityView view in views)
                    view.Link(entity);
            
            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Register(collider, entity);
        }

        public void Cleanup(Entity entity)
        {
            var views = GetComponentsInChildren<EntityView>();
            if (views != null)
                foreach(EntityView view in views)
                    view.Cleanup(entity);
            
            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Unregister(collider);
            
            _linkedEntity = null;
        }
    }
}