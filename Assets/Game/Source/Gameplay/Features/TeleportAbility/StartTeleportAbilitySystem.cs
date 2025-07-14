using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.TeleportAbility
{
    public class StartTeleportAbilitySystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _useRequest;
        private ReactiveEvent _startEvent;
        private ReactiveVariable<bool> _inCastProcess;
        private ICompositeCondition _canStart;

        private ISubscription _useRequestSubscription;
        
        public void OnInit(Entity entity)
        {
            _useRequest = entity.TeleportAbilityStartRequest;
            _startEvent = entity.TeleportAbilityStartEvent;
            _inCastProcess = entity.InTeleportAbilityCastProcess;
            _canStart = entity.CanUseTeleportAbility;
            
            _useRequestSubscription = _useRequest.Subscribe(OnUseRequest);
        }

        private void OnUseRequest()
        {
            if (_canStart.Evaluate())
            {
                _inCastProcess.Value = true;
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
            _useRequestSubscription.Unsubscribe();
        }
    }
}