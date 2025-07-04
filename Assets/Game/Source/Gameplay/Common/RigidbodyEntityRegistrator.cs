using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Game.Gameplay.Common
{
    public class RigidbodyEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            entity.AddComponent(new RigidbodyComponent() { Value = GetComponent<Rigidbody>() });
        }
    }
}