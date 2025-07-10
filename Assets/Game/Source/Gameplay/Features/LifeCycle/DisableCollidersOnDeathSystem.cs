using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.LifeCycle
{
    public class DisableCollidersOnDeathSystem : IInitializableSystem, IDisposableSystem
    {
        private List<Collider> _colliders;
        private ReactiveVariable<bool> _isDead;
        
        private ISubscription _isDeadChangedSubscription;

        public void OnInit(Entity entity)
        {
            _colliders = entity.DisableCollidersOnDeath;
            _isDead = entity.IsDead;

            _isDeadChangedSubscription = _isDead.Subscribe(OnIsDeadChanged);
        }

        private void OnIsDeadChanged(bool _, bool isDead)
        {
            if (isDead)
                foreach (var collider in _colliders)
                    collider.enabled = false;
        }

        public void OnDispose()
        {
            _isDeadChangedSubscription.Unsubscribe();
        }
    }
}