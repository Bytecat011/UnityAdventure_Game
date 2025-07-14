using Game.Gameplay.EntitiesCore;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.TeleportAbility
{
    public class TeleportAbilityEnergyCost : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class TeleportAbilityRange : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class TeleportAbilityStartRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class TeleportAbilityStartEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class CanUseTeleportAbility : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class TeleportAbilityEndEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class TeleportAbilityCastInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class TeleportAbilityCastCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class InTeleportAbilityCastProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}