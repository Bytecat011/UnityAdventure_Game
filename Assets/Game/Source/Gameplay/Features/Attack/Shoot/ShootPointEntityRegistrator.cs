using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Game.Gameplay.Features.Attack.Shoot
{
    public class ShootPointEntityRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _shootPoint;
        
        public override void Register(Entity entity)
        {
            entity.AddShootPoint(_shootPoint);
        }
    }
}