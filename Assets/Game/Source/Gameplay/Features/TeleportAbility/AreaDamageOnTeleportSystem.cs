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
        private Transform _transform;
        
        private IDisposable _teleportEndSubscription;

        public void OnInit(Entity entity)
        {
            _areaDamageRequest = entity.AreaDamageRequest;
            _teleportEndEvent = entity.TeleportAbilityEndEvent;
            _transform = entity.Transform;

            _teleportEndSubscription = _teleportEndEvent.Subscribe(OnTeleportEnded);
        }

        private void OnTeleportEnded()
        {
            _areaDamageRequest.Notify(_transform.position);
        }

        public void OnDispose()
        {
            _teleportEndSubscription.Dispose();
        }
    }
}