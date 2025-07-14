using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.TeleportAbility
{
    public class AreaDamageOnTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<Vector3> _areaDamageRequest;
        private ReactiveEvent _teleportEndEvent;
        private Rigidbody _rigidbody;
        
        private ISubscription _teleportEndSubscription;

        public void OnInit(Entity entity)
        {
            _areaDamageRequest = entity.AreaDamageRequest;
            _teleportEndEvent = entity.TeleportAbilityEndEvent;
            _rigidbody = entity.Rigidbody;

            _teleportEndSubscription = _teleportEndEvent.Subscribe(OnTeleportEnded);
        }

        private void OnTeleportEnded()
        {
            _areaDamageRequest.Notify(_rigidbody.position);
        }

        public void OnDispose()
        {
            _teleportEndSubscription.Unsubscribe();
        }
    }
}