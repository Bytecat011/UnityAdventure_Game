using Game.Gameplay.EntitiesCore;
using Game.Utility.Conditions;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.ApplyDamage
{
    public class TakeDamageRequest : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class TakeDamageEvent : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }
    
    public class CanApplyDamage : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}