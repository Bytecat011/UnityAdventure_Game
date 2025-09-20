using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Game.Gameplay.Features.LifeCycle
{
    public class HealthBarPointRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _point;
        
        public override void Register(Entity entity)
        {
            entity.AddHealthBarPoint(_point);
        }
    }
}