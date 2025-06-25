using Game.Configs;
using Game.Core.DI;
using Game.Data;
using Game.Meta.Features.LevelStatistics;
using Game.Meta.Features.Resources;
using Game.Meta.MainMenu;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using Unity.VisualScripting;

namespace Game.Meta.Core
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateLevelSelector);
            container.RegisterAsSingle(CreaLevelStatisticsResetter);
            container.RegisterAsSingle(CreatePlayerGoldDisplayService);
            container.RegisterAsSingle(CreateLevelsStatisticsDisplayService);
        }

        private static LevelsStatisticsDisplayService CreateLevelsStatisticsDisplayService(DIContainer c)
            => new LevelsStatisticsDisplayService(
                c.Resolve<ICoroutineRunner>(),
                c.Resolve<LevelStatisticsService>()
            );
        
        private static PlayerGoldDisplayService CreatePlayerGoldDisplayService(DIContainer c)
            => new PlayerGoldDisplayService(
                c.Resolve<ICoroutineRunner>(),
                c.Resolve<ResourceStorage>()
            );

        private static LevelsStatisticsResetter CreaLevelStatisticsResetter(DIContainer c)
        {
            var configManager = c.Resolve<ConfigManager>();

            return new LevelsStatisticsResetter(
                c.Resolve<ICoroutineRunner>(),
                c.Resolve<LevelStatisticsService>(),
                c.Resolve<ResourceStorage>(),
                configManager.GetConfig<EconomyConfig>(),
                c.Resolve<PlayerDataProvider>()
            );
        }

        private static LevelSelector CreateLevelSelector(DIContainer c)
        {
            return new LevelSelector(c.Resolve<ICoroutineRunner>(), c.Resolve<SceneSwitcherService>());
        }
    }
}