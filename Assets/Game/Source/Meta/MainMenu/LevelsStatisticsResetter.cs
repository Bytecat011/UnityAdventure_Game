using System;
using System.Collections;
using Game.Configs;
using Game.Data;
using Game.Meta.Features.LevelStatistics;
using Game.Meta.Features.Resources;
using Game.Utility.CoroutineManagement;
using UnityEngine;

namespace Game.Meta.MainMenu
{
    public class LevelsStatisticsResetter : IDisposable
    {
        private const KeyCode ResetKeyCode = KeyCode.R;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LevelStatisticsService _levelStatisticsService;
        private readonly ResourceStorage _resourceStorage;
        private readonly EconomyConfig _economyConfig;
        private readonly PlayerDataProvider _playerDataProvider;

        private Coroutine _handleInputTsk;

        public LevelsStatisticsResetter(
            ICoroutineRunner coroutineRunner,
            LevelStatisticsService levelStatisticsService, 
            ResourceStorage resourceStorage,
            EconomyConfig economyConfig,
            PlayerDataProvider playerDataProvider)
        {
            _coroutineRunner = coroutineRunner;
            _levelStatisticsService = levelStatisticsService;
            _resourceStorage = resourceStorage;
            _economyConfig = economyConfig;
            _playerDataProvider = playerDataProvider;
        }

        public void Start()
        {
            _handleInputTsk = _coroutineRunner.StartTask(HandleInputTask());
        }

        private IEnumerator HandleInputTask()
        {
            Debug.Log($"You can rest level statistics for {_economyConfig.ResetStatisticsCost} gold by press {ResetKeyCode} key");
            
            while (true)
            {
                if (Input.GetKeyDown(ResetKeyCode))
                {
                    if (TryResetLevelsStatistics())
                    {
                        Debug.Log("Level statistics reset successfully");
                        yield return _coroutineRunner.StartTask(_playerDataProvider.Save());
                    }
                    else
                        Debug.Log("Level statistics reset failed. Not enough gold");
                }
                yield return null;
            }
        }

        private bool TryResetLevelsStatistics()
        {
            if (_resourceStorage.IsEnough(ResourceType.Gold, _economyConfig.ResetStatisticsCost))
            {
                _resourceStorage.Spend(ResourceType.Gold, _economyConfig.ResetStatisticsCost);
                _levelStatisticsService.Reset();
                return true;
            }
            return false;
        }
        
        public void Dispose()
        {
            if (_handleInputTsk != null)
                _coroutineRunner.StopTask(_handleInputTsk);
        }
    }
}