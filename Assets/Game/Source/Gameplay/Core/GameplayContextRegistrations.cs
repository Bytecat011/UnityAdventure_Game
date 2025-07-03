using System.Linq;
using Game.Configs.Gameplay.Levels;
using Game.Core.DI;
using Game.Data;
using Game.Gameplay.TypingGameplay;
using Game.Meta.Features.LevelStatistics;
using Game.Meta.Features.Resources;
using Game.UI;
using Game.UI.Core;
using Game.UI.Gameplay;
using Game.Utility.Assets;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;

namespace Game.Gameplay.Core
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle((c) => CreateLevelConfig(c, args)).NonLazy();
            container.RegisterAsSingle(CreateWordGeneratorService);
            container.RegisterAsSingle(CreateGameMode).NonLazy();
            container.RegisterAsSingle((c) => CreateGameModeFlowController(c, args)).NonLazy();
            container.RegisterAsSingle(CreateLevelRewardHandler).NonLazy();
            
            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateGameplayPopupService);
        }

        private static LevelRewardHandler CreateLevelRewardHandler(DIContainer c)
            => new LevelRewardHandler(
                c.Resolve<ConfigManager>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<TypingGameMode>(),
                c.Resolve<ResourceStorage>(),
                c.Resolve<ICoroutineRunner>());
        
        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<GameplayUIRoot>());
        }
        
        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            var resourceAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            var gameplayUIRootPrefab = resourceAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return UnityEngine.Object.Instantiate(gameplayUIRootPrefab);
        }

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
        {
            return new GameplayPresentersFactory(c);
        }

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

            GameplayScreenPresenter presenter = c
                .Resolve<GameplayPresentersFactory>()
                .CreateGameplayScreen(view);

            return presenter;
        }
        
        private static GameplayFlowController CreateGameModeFlowController(DIContainer c, GameplayInputArgs args)
        {
            return new GameplayFlowController(
                c.Resolve<TypingGameMode>(),
                c.Resolve<LevelStatisticsService>(),
                c.Resolve<ICoroutineRunner>(),
                c.Resolve<SceneSwitcherService>(),
                args.GameplayMode);
        }
        
        private static TypingGameMode CreateGameMode(DIContainer c)
        {
            var wordGenerator = c.Resolve<WordGeneratorService>();
            var levelConfig = c.Resolve<LevelConfig>();

            return new TypingGameMode(wordGenerator.GenerateWord(levelConfig.WordLength));
        }
        
        private static LevelConfig CreateLevelConfig(DIContainer c, GameplayInputArgs args)
        {
            var configManager = c.Resolve<ConfigManager>();

            var levelsListConfig = configManager.GetConfig<LevelsListConfig>();
            
            return levelsListConfig.Levels.First(l => l.GameplayMode == args.GameplayMode);
        }
        
        private static WordGeneratorService CreateWordGeneratorService(DIContainer c)
        {
            var config = c.Resolve<LevelConfig>();

            return new WordGeneratorService(config.AllowedCharacters);
        }
    }
}