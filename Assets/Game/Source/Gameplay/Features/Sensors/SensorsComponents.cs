using Game.Gameplay.EntitiesCore;
using Game.Utility;
using UnityEngine;

namespace Game.Gameplay.Features.Sensors
{
    public class BodyCollider : IEntityComponent
    {
        public CapsuleCollider Value;
    }

    public class ContactsDetectingMask : IEntityComponent
    {
        public LayerMask Value;
    }

    public class ContactColliderBuffer : IEntityComponent
    {
        public Buffer<Collider> Value;
    }
    
    public class ContactEntitiesBuffer : IEntityComponent
    {
        public Buffer<Entity> Value;
    }
}