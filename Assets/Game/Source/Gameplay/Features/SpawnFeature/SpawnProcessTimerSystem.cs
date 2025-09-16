using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.SpawnFeature
{
    public class SpawnProcessTimerSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<float> _currentTime;
        
        private ReactiveVariable<bool> _inSpawnProcess;
        
        public void OnInit(Entity entity)
        {
            _initialTime = entity.SpawnInitialTime;
            _currentTime = entity.SpawnCurrentTime;
            _inSpawnProcess = entity.InSpawnProcess;

            _currentTime.Value = 0f;
            _inSpawnProcess.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inSpawnProcess.Value == false)
                return;
            
            _currentTime.Value += deltaTime;
            
            if (_currentTime.Value >= _initialTime.Value)
                _inSpawnProcess.Value = false;
        }
    }
}