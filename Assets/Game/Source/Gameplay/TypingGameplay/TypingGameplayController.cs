using System;
using System.Collections;
using Game.Core.DI;
using Game.Gameplay.Config;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using UnityEngine;

namespace Game.Gameplay.TypingGameplay
{
    public class TypingGameplayController : IDisposable
    {
        private const KeyCode ExitKey = KeyCode.Space;
        private const KeyCode RestartKey = KeyCode.Space;

        public event Action<bool> GameFinished;
        
        private readonly WordGeneratorService _wordGenerator;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameplayConfig _config;
        private TypingGameMode _gameMode;
        private bool? _gameResult;

        public TypingGameplayController(
            GameplayConfig config,
            WordGeneratorService wordGenerator, 
            ICoroutineRunner coroutineRunner)
        {
            _config = config;
            _wordGenerator = wordGenerator;
            _coroutineRunner = coroutineRunner;
        }
        
        public void Start()
        {
            string word = _wordGenerator.GenerateWord(_config.WordLength);
            
            _gameMode = new TypingGameMode(word);
            _gameMode.Win += OnGameWin;
            _gameMode.Lose += OnGameLose;
            
            _coroutineRunner.StartTask(GameplayCoroutine());
        }
        
        private void OnGameLose()
        {
            _gameResult = false;
        }

        private void OnGameWin()
        {
            _gameResult = true;
        }

        private IEnumerator GameplayCoroutine()
        {
            _gameMode.Start();
            
            Debug.Log($"Game started. Target word {_gameMode.TargetWord}");

            while (_gameResult.HasValue == false)
            {
                _gameMode.Update();
                yield return null;
            }

            if (_gameResult.Value == false)
            {
                Debug.Log($"You lose! Press {RestartKey} to restart.");
                yield return new WaitUntil(() => Input.GetKeyDown(RestartKey));
            }

            if (_gameResult.Value == true)
            {
                Debug.Log($"You win! Press {ExitKey} to restart.");
                yield return new WaitUntil(() => Input.GetKeyDown(ExitKey));
            }
            
            GameFinished?.Invoke(_gameResult.Value);
        }
        
        public void Dispose()
        {
            if (_gameMode != null)
            {
                _gameMode.Win -= OnGameWin;
                _gameMode.Lose -= OnGameLose;
            }
        }
    }
}