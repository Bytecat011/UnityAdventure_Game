using System;
using Game.Gameplay.Features.StagesFeature;
using Game.UI.CommonViews;
using Game.UI.Core;

namespace Game.UI.Gameplay.Stages
{
    public class StagePresenter : IPresenter
    {
        private readonly IconTextView _view;
        private readonly StageProviderService _stageProviderService;
        
        private IDisposable _currentStageNumberSubscription;

        public StagePresenter(IconTextView view, StageProviderService stageProviderService)
        {
            _view = view;
            _stageProviderService = stageProviderService;
        }

        public void Initialize()
        {
            _currentStageNumberSubscription = _stageProviderService.CurrentStageNumber.Subscribe(OnStageIndexChanged);

            UpdateStageNumber();
        }
        
        public void Dispose()
        {
            _currentStageNumberSubscription?.Dispose();
        }

        private void UpdateStageNumber()
        {
            _view.SetText($"{_stageProviderService.CurrentStageNumber.Value} / {_stageProviderService.StagesCount}");
        }

        private void OnStageIndexChanged(int arg1, int arg2) => UpdateStageNumber();
    }
}