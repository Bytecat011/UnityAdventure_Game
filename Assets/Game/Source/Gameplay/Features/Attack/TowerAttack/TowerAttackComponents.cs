using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.Attack.TowerAttack
{
    public class TowerAttackDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}