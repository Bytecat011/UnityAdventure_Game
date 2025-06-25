using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System;
using System.Collections;
using Game.Configs;
using Game.Data;
using Game.Gameplay.Config;
using Game.Gameplay.TypingGameplay;
using Game.Meta.Features.LevelStatistics;
using Game.Meta.Features.Resources;
using Game.Utility.Configs;
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
            var coroutineRunner = _container.Resolve<ICoroutineRunner>();
            
            var levelStatisticsService = _container.Resolve<LevelStatisticsService>();
            levelStatisticsService.HandleLevelResult(won);
            
            HandleGoldForLevelResult(won);

            coroutineRunner.StartTask(_container.Resolve<PlayerDataProvider>().Save());
            
            string nextScene = won == true ? Scenes.MainMenu : Scenes.Gameplay;

            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            coroutineRunner.StartTask(sceneSwitcher.SwitchTo(nextScene, _inputArgs));
        }

        private void HandleGoldForLevelResult(bool won)
        {
            var economyConfig = _container.Resolve<ConfigManager>().GetConfig<EconomyConfig>();
            var resourceStorage = _container.Resolve<ResourceStorage>();
            if (won)
                resourceStorage.Add(ResourceType.Gold, economyConfig.GainGoldAmountForWin);
            else if (resourceStorage.IsEnough(ResourceType.Gold, economyConfig.LoseGoldAmountForLose))
                resourceStorage.Spend(ResourceType.Gold, economyConfig.LoseGoldAmountForLose);
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