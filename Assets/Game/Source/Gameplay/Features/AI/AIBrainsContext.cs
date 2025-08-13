using System;
using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;

namespace Game.Gameplay.Features.AI
{
    public class AIBrainsContext : IDisposable
    {
        private readonly List<EntityToBrain> _entityToBrains = new();

        public void SetFor(Entity entity, IBrain brain)
        {
            foreach (EntityToBrain item in _entityToBrains)
            {
                if (item.Entity == entity)
                {
                    item.Brain.Disable();
                    item.Brain.Dispose();
                    item.Brain = brain;
                    item.Brain.Enable();
                    return;
                }
            }
            
            _entityToBrains.Add(new EntityToBrain(entity, brain));
            brain.Enable();
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _entityToBrains.Count; i++)
            {
                if (_entityToBrains[i].Entity.IsInit == false)
                {
                    int lastIndex = _entityToBrains.Count - 1;
                    
                    _entityToBrains[i].Brain.Dispose();
                    _entityToBrains[i] = _entityToBrains[lastIndex];
                    _entityToBrains.RemoveAt(lastIndex);
                    i--;
                    continue;
                }
                _entityToBrains[i].Brain.Update(deltaTime);
            }
        }
        
        private class EntityToBrain
        {
            public Entity Entity;
            public IBrain Brain;

            public EntityToBrain(Entity entity, IBrain brain)
            {
                Entity = entity;
                Brain = brain;
            }
        }

        public void Dispose()
        {
            foreach (var entityToBrain in _entityToBrains)
            {
                entityToBrain.Brain.Dispose();
            }
            _entityToBrains.Clear();
        }
    }
}