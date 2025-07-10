using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility;
using UnityEngine;

namespace Game.Gameplay.Features.Sensors
{
    public class BodyContactsDetectingSystem : IInitializableSystem, IUpdatableSystem
    {
        private Buffer<Collider> _contacts;
        private LayerMask _mask;

        private CapsuleCollider _body;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactColliderBuffer;
            _mask = entity.ContactsDetectingMask;

            _body = entity.BodyCollider;
        }

        public void OnUpdate(float deltaTime)
        {
            _contacts.Count = Physics.OverlapCapsuleNonAlloc(
                _body.bounds.min,
                _body.bounds.max,
                _body.radius,
                _contacts.Items,
                _mask,
                QueryTriggerInteraction.Ignore);

            _contacts.TryRemove(_body);
        }
    }
}