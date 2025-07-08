using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Game.Gameplay.Features.Sensors
{
    public class BodyColliderRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private CapsuleCollider _body;

        public override void Register(Entity entity)
        {
            entity.AddBodyCollider(_body);
        }
    }
}