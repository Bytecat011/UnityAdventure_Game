using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Sensors
{
    public class DeathMaskTouchDetectorSystem : IInitializableSystem, IUpdatableSystem
    {
        private Buffer<Collider> _contacts;
        private ReactiveVariable<bool> _isTouchDeathMask;
        private LayerMask _deathMask;
        
        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactColliderBuffer;
            _isTouchDeathMask = entity.IsTouchDeathMask;
            _deathMask = entity.DeathMask;
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                if (MatchWithDeathLayer(_contacts.Items[i]))
                {
                    _isTouchDeathMask.Value = true;
                    return;
                }
            }
            
            _isTouchDeathMask.Value = false;
        }

        private bool MatchWithDeathLayer(Collider collider)
            => ((1 << collider.gameObject.layer) & _deathMask) != 0;
    }
}