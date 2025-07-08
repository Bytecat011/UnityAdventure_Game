using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Conditions;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.LifeCycle
{
    public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesWorld _entitiesWorld;
        
        private Entity _entity;

        private ICompositeCondition _mustSelfRelease;


        public SelfReleaseSystem(EntitiesWorld entitiesWorld)
        {
            _entitiesWorld = entitiesWorld;
        }
        
        public void OnInit(Entity entity)
        {
            _entity = entity;
            _mustSelfRelease = entity.MustSelfRelease;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_mustSelfRelease.Evaluate())
            {
                _entitiesWorld.Release(_entity);
            }
        }
    }
}