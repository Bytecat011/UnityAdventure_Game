using System;
using Game.Configs.Gameplay.Levels;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.StagesFeature
{
    public class StageProviderService : IDisposable
    {
        private ReactiveVariable<int> _currentStageNumber = new();
        
        private LevelConfig _levelConfig;
        private StagesFactory _stagesFactory;
        
        private IStage _currentStage;

        public StageProviderService(
            LevelConfig levelConfig, 
            StagesFactory stagesFactory)
        {
            _levelConfig = levelConfig;
            _stagesFactory = stagesFactory;
        }
        
        public IReactiveVariable<int> CurrentStageNumber => _currentStageNumber;

        public int StagesCount => _levelConfig.StageConfigs.Count;

        public bool HasNextStage() => CurrentStageNumber.Value < StagesCount;
        
        public void SwitchToNext()
        {
            if (HasNextStage() == false)
                throw new InvalidOperationException();
            
            if (_currentStage != null)
                CleanupCurrent();
            
            _currentStageNumber.Value++;
            
            _currentStage = _stagesFactory.Create(_levelConfig.StageConfigs[_currentStageNumber.Value - 1]);
        }

        public void StartCurrent() => _currentStage.Start();
        
        public void UpdateCurrent(float deltaTime) => _currentStage.Update(deltaTime);
        
        public void CleanupCurrent() => _currentStage.Cleanup();

        public void Dispose()
        {
            _currentStage?.Dispose();
        }
    }
}