using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.TeleportAbility
{
    public class TeleportAbilityCastSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<bool> _inUseProcess;
        private ReactiveEvent _startEvent;

        private IDisposable _startEventSubscription;
        
        public void OnInit(Entity entity)
        {
            _currentTime = entity.TeleportAbilityCastCurrentTime;
            _initialTime = entity.TeleportAbilityCastInitialTime;
            _inUseProcess = entity.InTeleportAbilityCastProcess;
            _startEvent = entity.TeleportAbilityStartEvent;

            _startEventSubscription = _startEvent.Subscribe(OnStartCastProcess);
        }

        private void OnStartCastProcess()
        {
            _currentTime.Value = _initialTime.Value;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inUseProcess.Value == false)
                return;
            
            _currentTime.Value -= deltaTime;
        }

        public void OnDispose()
        {
            _startEventSubscription.Dispose();
        }
    }
}