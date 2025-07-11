using Game.Gameplay.EntitiesCore;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    public class StartAttackRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class StartAttackEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class CanStartAttack : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class EndAttackEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class AttackProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class AttackProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class InAttackProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    public class AttackDelayTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class AttackDelayEndEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class InstantAttackDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class ShootPoint : IEntityComponent
    {
        public Transform Value;
    }
    
    public class MustCancelAttack : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class AttackCancelEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class AttackCooldownInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class AttackCooldownCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class InAttackCooldown : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}