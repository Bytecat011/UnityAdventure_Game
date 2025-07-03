using System;
using Game.Configs.Meta.Resources;
using Game.Data;
using Game.Meta.Features.Resources;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;

namespace Game.Gameplay.TypingGameplay
{
    public class LevelRewardHandler : IDisposable
    {
        private readonly ConfigManager _configManager;
        private readonly ResourceStorage _resourceStorage;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly TypingGameMode _gameMode;
        private readonly ICoroutineRunner _coroutineRunner;

        public LevelRewardHandler(
            ConfigManager configManager, 
            PlayerDataProvider playerDataProvider,
            TypingGameMode gameMode,
            ResourceStorage resourceStorage, 
            ICoroutineRunner coroutineRunner)
        {
            _configManager = configManager;
            _playerDataProvider = playerDataProvider;
            _gameMode = gameMode;
            _resourceStorage = resourceStorage;
            _coroutineRunner = coroutineRunner;

            Initialize();
        }

        private void Initialize()
        {
            _gameMode.Win += OnWin;
            _gameMode.Lose += OnLose;
        }

        private void OnWin()
        {
            var economyConfig = _configManager.GetConfig<EconomyConfig>();
            
            _resourceStorage.Add(ResourceType.Gold, economyConfig.GainGoldAmountForWin);
            
            _coroutineRunner.StartTask(_playerDataProvider.Save());
        }

        private void OnLose()
        {
            var economyConfig = _configManager.GetConfig<EconomyConfig>();
            
            if(_resourceStorage.IsEnough(ResourceType.Gold, economyConfig.LoseGoldAmountForLose))
                _resourceStorage.Spend(ResourceType.Gold, economyConfig.LoseGoldAmountForLose);
            
            _coroutineRunner.StartTask(_playerDataProvider.Save());
        }

        public void Dispose()
        {
            _gameMode.Win -= OnWin;
            _gameMode.Lose -= OnLose;
        }
    }
}