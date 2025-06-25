using Game.Core.DI;
using Game.Gameplay.Config;
using Game.Gameplay.TypingGameplay;
using Game.Utility.Configs;

namespace Game.Gameplay.Core
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle((c) => CreateGameplayConfig(c, args));
            container.RegisterAsSingle(CreateWordGeneratorService);
        }

        private static GameplayConfig CreateGameplayConfig(DIContainer c, GameplayInputArgs args)
        {
            var configManager = c.Resolve<ConfigManager>();

            var levelsConfig = configManager.GetConfig<LevelsConfig>();

            return levelsConfig.GetConfigFor(args.GameplayMode);
        }
        
        private static WordGeneratorService CreateWordGeneratorService(DIContainer c)
        {
            var config = c.Resolve<GameplayConfig>();

            return new WordGeneratorService(config.AllowedCharacters);
        }
    }
}