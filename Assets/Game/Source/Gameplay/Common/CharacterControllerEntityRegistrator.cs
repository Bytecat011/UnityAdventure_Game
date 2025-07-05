using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Game.Gameplay.Common
{
    public class CharacterControllerEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            entity.AddCharacterController(GetComponent<CharacterController>());
        }
    }
}