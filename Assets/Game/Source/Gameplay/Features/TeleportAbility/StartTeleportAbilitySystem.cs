using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.TeleportAbility
{
    public class StartTeleportAbilitySystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<Vector3> _useRequest;
        private ReactiveEvent _startEvent;
        private ReactiveVariable<bool> _inCastProcess;
        private ReactiveVariable<Vector3> _targetPosition;
        private ICompositeCondition _canStart;

        private IDisposable _useRequestSubscription;
        
        public void OnInit(Entity entity)
        {
            _useRequest = entity.TeleportAbilityStartRequest;
            _startEvent = entity.TeleportAbilityStartEvent;
            _inCastProcess = entity.InTeleportAbilityCastProcess;
            _canStart = entity.CanUseTeleportAbility;
            _targetPosition = entity.TeleportAbilityTarget;
            
            _useRequestSubscription = _useRequest.Subscribe(OnUseRequest);
        }

        private void OnUseRequest(Vector3 targetPosition)
        {
            if (_canStart.Evaluate())
            {
                _inCastProcess.Value = true;
                _targetPosition.Value = targetPosition;
                _startEvent.Notify();
                Debug.Log("Start Teleport Ability");
            }
            else
            {
                Debug.Log("Can't start Teleport Ability");
            }
        }

        public void OnDispose()
        {
            _useRequestSubscription.Dispose();
        }
    }
}