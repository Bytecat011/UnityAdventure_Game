using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.LifeCycle
{
    public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesWorld _entitiesWorld;
        
        private Entity _entity;

        private ReactiveVariable<bool> _isDead;


        public SelfReleaseSystem(EntitiesWorld entitiesWorld)
        {
            _entitiesWorld = entitiesWorld;
        }
        
        public void OnInit(Entity entity)
        {
            _entity = entity;
            _isDead = entity.IsDead;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
            {
                _entitiesWorld.Release(_entity);
            }
        }
    }
}