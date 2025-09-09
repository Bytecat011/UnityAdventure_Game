using System;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.MainHero
{
    public class MainHeroHolderService : IInitializable, IDisposable
    {
        private EntitiesWorld _entitiesWorld;
        private ReactiveEvent<Entity> _heroRegistered = new();
        
        private Entity _mainHero;

        public MainHeroHolderService(EntitiesWorld entitiesWorld)
        {
            _entitiesWorld = entitiesWorld;
        }
        
        public IReadOnlyEvent<Entity> HeroRegistered => _heroRegistered;
        
        public Entity MainHero => _mainHero;

        public void Initialize()
        {
            _entitiesWorld.Added += OnEntityAdded;
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.HasComponent<IsMainHero>())
            {
                _entitiesWorld.Added -= OnEntityAdded;
                _mainHero = entity;
                _heroRegistered.Notify(_mainHero);
            }
        }

        public void Dispose()
        {
            _entitiesWorld.Added -= OnEntityAdded;
        }
    }
}