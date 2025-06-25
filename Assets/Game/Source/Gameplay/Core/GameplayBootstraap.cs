using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System;
using System.Collections;
using Game.Gameplay.Config;
using Game.Gameplay.TypingGameplay;
using Game.Utility.CoroutineManagement;

namespace Game.Gameplay.Core
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private TypingGameplayController _typingGameplayController;

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
            _typingGameplayController = new TypingGameplayController(
                _container.Resolve<GameplayConfig>(),
                _container.Resolve<WordGeneratorService>(),
                _container.Resolve<ICoroutineRunner>());   
            
            _typingGameplayController.GameFinished += HandleGameplayResult;
            
            yield break;
        }

        private void HandleGameplayResult(bool won)
        {
            string nextScene = won == true ? Scenes.MainMenu : Scenes.Gameplay;

            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            _container.Resolve<ICoroutineRunner>().StartTask(sceneSwitcher.SwitchTo(nextScene, _inputArgs));
        }
        
        public override void Run()
        {
            _typingGameplayController.Start();
        }

        private void OnDestroy()
        {
            if (_typingGameplayController != null)
            {
                _typingGameplayController.Dispose();
                _typingGameplayController.GameFinished -= HandleGameplayResult;
            }
        }
    }
}