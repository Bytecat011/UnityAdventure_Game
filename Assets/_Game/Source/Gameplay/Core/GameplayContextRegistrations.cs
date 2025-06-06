using Game.Core.DI;
using Game.Gameplay.Config;
using Game.Gameplay.TypingGameplay;
using Game.Meta.Config;
using Game.Utility.Configs;

namespace Game.Gameplay.Core
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle((c) => CreateGameplayConfigService(c, args));
            container.RegisterAsSingle(CreateWordGeneratorService);
        }

        private static GameplayConfigService CreateGameplayConfigService(DIContainer c, GameplayInputArgs args)
        {
            var configManager = c.Resolve<ConfigManager>();

            var levelsList = configManager.GetConfig<LevelsListConfig>();

            return new GameplayConfigService(levelsList.Levels[args.LevelNumber].Config);
        }

        private static WordGeneratorService CreateWordGeneratorService(DIContainer c)
        {
            var config = c.Resolve<GameplayConfigService>();

            return new WordGeneratorService(config.Config.CharsList);
        }
    }
}