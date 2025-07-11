using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.Attack
{
    public class AttackProcessTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inAttackProcess;
        private ReactiveEvent _startAttackEvent;

        private ISubscription _startAttackEventSubscription;
        
        public void OnInit(Entity entity)
        {
            _currentTime = entity.AttackProcessCurrentTime;
            _inAttackProcess = entity.InAttackProcess;
            _startAttackEvent = entity.StartAttackEvent;

            _startAttackEventSubscription = _startAttackEvent.Subscribe(OnStartAttackProcess);
        }

        private void OnStartAttackProcess()
        {
            _currentTime.Value = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAttackProcess.Value == false)
                return;
            
            _currentTime.Value += deltaTime;
        }

        public void OnDispose()
        {
            _startAttackEventSubscription.Unsubscribe();
        }
    }
}