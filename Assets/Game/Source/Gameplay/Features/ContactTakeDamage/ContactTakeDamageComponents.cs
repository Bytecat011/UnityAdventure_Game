using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.ContactTakeDamage
{
    public class BodyContactDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}