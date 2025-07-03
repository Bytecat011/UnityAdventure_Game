using Game.Configs.Meta.Resources;
using Game.Data;
using Game.Meta.Features.LevelStatistics;
using Game.Meta.Features.Resources;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;
using UnityEngine;

namespace Game.UI.LevelStatistics
{
    public class ResetLevelStatisticsPresenter : IPresenter
    {
        private readonly ButtonView _buttonView;
        private readonly LevelStatisticsService _levelStatisticsService;
        private readonly ResourceStorage _resourceStorage;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ConfigManager _configManager;
        private readonly ICoroutineRunner _coroutineRunner;

        public ResetLevelStatisticsPresenter(
            LevelStatisticsService levelStatisticsService,
            ResourceStorage resourceStorage,
            PlayerDataProvider playerDataProvider, 
            ConfigManager configManager,
            ICoroutineRunner coroutineRunner,
            ButtonView buttonView)
        {
            _buttonView = buttonView;
            _levelStatisticsService = levelStatisticsService;
            _resourceStorage = resourceStorage;
            _playerDataProvider = playerDataProvider;
            _configManager = configManager;
            _coroutineRunner = coroutineRunner;
        }

        public void Initialize()
        {
            _buttonView.Clicked += OnResetButtonClicked;
        }

        public void Dispose()
        {
            _buttonView.Clicked -= OnResetButtonClicked;
        }
        
        private void OnResetButtonClicked()
        {
            var economyConfig = _configManager.GetConfig<EconomyConfig>();

            if (_resourceStorage.IsEnough(ResourceType.Gold, economyConfig.ResetStatisticsGoldCost))
            {
                _resourceStorage.Spend(ResourceType.Gold, economyConfig.ResetStatisticsGoldCost);
                _levelStatisticsService.Reset();
                _coroutineRunner.StartTask(_playerDataProvider.Save());
            }
            else
            {
                Debug.Log("Not enough gold to reset levels statistics");
            }
        }
    }
}