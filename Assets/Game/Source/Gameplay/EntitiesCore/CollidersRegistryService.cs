using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.EntitiesCore
{
    public class CollidersRegistryService
    {
        private readonly Dictionary<Collider, Entity> _colliderToEntity = new();

        public void Register(Collider collider, Entity entity)
        {
            _colliderToEntity.Add(collider, entity);
        }

        public void Unregister(Collider collider)
        {
            _colliderToEntity.Remove(collider);
        }

        public Entity GetBy(Collider collider)
        {
            return _colliderToEntity.GetValueOrDefault(collider);
        }
    }
}