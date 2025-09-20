using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.LifeCycle
{
    public class CurrentHealth : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class MaxHealth : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class HealthBarPoint : IEntityComponent
    {
        public Transform Value;
    }
    
    public class IsDead : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    public class MustDie : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class MustSelfRelease : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class DeathProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class DeathProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class InDeathProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    public class DisableCollidersOnDeath : IEntityComponent
    {
        public List<Collider> Value;
    }
}