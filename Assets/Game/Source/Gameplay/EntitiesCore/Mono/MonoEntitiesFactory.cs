using System;
using System.Collections.Generic;
using Game.Core.DI;
using Game.Utility.Assets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay.EntitiesCore.Mono
{
    public class MonoEntitiesFactory : IInitializable, IDisposable
    {
        private readonly ResourcesAssetsLoader _resources;
        
        private readonly EntitiesWorld _entitiesWorld;

        private readonly Dictionary<Entity, MonoEntity> _entityToMono = new();
        
        public MonoEntitiesFactory(ResourcesAssetsLoader resources, EntitiesWorld entitiesWorld)
        {
            _resources = resources;
            _entitiesWorld = entitiesWorld;
        }

        public MonoEntity Create(Entity entity, Vector3 position, string path)
        {
            MonoEntity prefab = _resources.Load<MonoEntity>(path);
            
            MonoEntity viewInstance = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity, null);
            
            viewInstance.Setup(entity);
            
            return viewInstance;
        }

        public void Initialize()
        {
            _entitiesWorld.Released += OnEntityReleased;
        }
        
        public void Dispose()
        {
            _entitiesWorld.Released -= OnEntityReleased;
            
            foreach (var entity in _entityToMono.Keys)
                CleanupFor(entity);
            
            _entityToMono.Clear();
        }

        private void OnEntityReleased(Entity entity)
        {
            CleanupFor(entity);

            _entityToMono.Remove(entity);
        }

        private void CleanupFor(Entity entity)
        {
            var monoEntity = _entityToMono[entity];
            monoEntity.Cleanup(entity);
            UnityEngine.Object.Destroy(monoEntity.gameObject);
        }
    }
}