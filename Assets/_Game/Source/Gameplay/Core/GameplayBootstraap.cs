using Game.Core;
using Game.Core.DI;
using Game.Gameplay.Config;
using Game.Gameplay.TypingGameplay;
using Game.Utility.CoroutineManagment;
using Game.Utility.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Gameplay.Core
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private const KeyCode ExitKey = KeyCode.Space;
        private const KeyCode RestartKey = KeyCode.Space;

        [field: SerializeField] private WordTypingView _wordTypingView;
        [field: SerializeField] private GameObject _winScreen;
        [field: SerializeField] private GameObject _loseScreen;

        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        private TypingGameMode _gameMode;
        private bool? _gameResult;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
            {
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");
            }

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(container, gameplayInputArgs);
        }

        public override IEnumerator Initialize()
        {
            WordGeneratorService wordGenerator = _container.Resolve<WordGeneratorService>();
            GameplayConfigService gameplayConfig = _container.Resolve<GameplayConfigService>();

            string word = wordGenerator.GenerateWord(gameplayConfig.Config.WordLength);

            _gameMode = new TypingGameMode(word);
            _gameMode.Win += OnGameWin;
            _gameMode.Lose += OnGameLose;

            _wordTypingView.Initialize(_gameMode);

            _winScreen.SetActive(false);
            _loseScreen.SetActive(false);

            yield break;
        }

        public override void Run()
        {
            ICoroutineRunner coroutineRunner = _container.Resolve<ICoroutineRunner>();
            coroutineRunner.StartTask(GameplayCoroutine());
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

            _wordTypingView.gameObject.SetActive(true);

            while (_gameResult.HasValue == false)
            {
                _gameMode.Update();
                yield return null;
            }

            _wordTypingView.gameObject.SetActive(false);

            if (_gameResult.Value == false)
            {
                _loseScreen.SetActive(true);
                yield return new WaitUntil(() => Input.GetKeyDown(RestartKey));
            }

            if (_gameResult.Value == true)
            {
                _winScreen.SetActive(true);
                yield return new WaitUntil(() => Input.GetKeyDown(ExitKey));
            }

            string nextScene = _gameResult.Value == true ? Scenes.MainMenu : Scenes.Gameplay;

            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            yield return sceneSwitcher.SwitchTo(nextScene, _inputArgs);
        }

        private void OnDestroy()
        {
            if (_gameMode != null)
            {
                _gameMode.Win -= OnGameWin;
                _gameMode.Lose -= OnGameLose;
            }
        }
    }
}