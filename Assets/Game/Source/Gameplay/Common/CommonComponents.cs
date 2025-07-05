using Game.Gameplay.EntitiesCore;
using UnityEngine;

namespace Game.Gameplay.Common
{
    public class TransformComponent : IEntityComponent
    {
        public Transform Value;
    }
    
    public class RigidbodyComponent : IEntityComponent
    {
        public Rigidbody Value;
    }

    public class CharacterControllerComponent : IEntityComponent
    {
        public CharacterController Value;
    }
}