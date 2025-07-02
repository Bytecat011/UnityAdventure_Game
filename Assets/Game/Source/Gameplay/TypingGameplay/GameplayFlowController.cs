using System;
using System.Collections;
using Game.Meta.Features.LevelStatistics;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using UnityEngine;

namespace Game.Gameplay.TypingGameplay
{
    public class GameplayFlowController : IDisposable
    {
        private readonly TypingGameMode _gameMode;
        private readonly LevelStatisticsService _levelStatisticsService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly GameplayModeType _gameplayMode;

        private Coroutine _updateGameTask;
        
        public GameplayFlowController(
            TypingGameMode gameMode,
            LevelStatisticsService levelStatisticsService, 
            ICoroutineRunner coroutineRunner, 
            SceneSwitcherService sceneSwitcher, 
            GameplayModeType gameplayMode)
        {
            _gameMode = gameMode;
            _levelStatisticsService = levelStatisticsService;
            _coroutineRunner = coroutineRunner;
            _sceneSwitcher = sceneSwitcher;
            _gameplayMode = gameplayMode;
        }

        public void Start()
        {
            _gameMode.Win += OnGameWin;
            _gameMode.Lose += OnGameLose;
            _gameMode.Start();

            _updateGameTask = _coroutineRunner.StartTask(UpdateGameModeTask());
        }

        public void Dispose()
        {
            _gameMode.Win -= OnGameWin;
            _gameMode.Lose -= OnGameLose;
            
            _coroutineRunner.StopTask(_updateGameTask);
        }

        private void OnGameWin()
        {
            HandleGameResult(true);
        }

        private void OnGameLose()
        {
            HandleGameResult(false);
        }

        private void HandleGameResult(bool win)
        {
            _levelStatisticsService.HandleLevelResult(win);

            _coroutineRunner.StartTask(_sceneSwitcher.SwitchTo(Scenes.MainMenu));
        }
        
        private IEnumerator UpdateGameModeTask()
        {
            while (true)
            {
                _gameMode.Update();
                yield return null;
            }
        }
    }
}