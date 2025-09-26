using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack.TowerAttack
{
    public class TowerAttackDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class TowerAttackTargetPoint : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }
}