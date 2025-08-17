using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;

namespace Game.Gameplay.Common
{
    public class TransformEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            entity.AddTransform(transform);
        }
    }
}