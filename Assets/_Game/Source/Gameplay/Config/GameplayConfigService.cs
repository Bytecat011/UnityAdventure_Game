namespace Game.Gameplay.Config
{
    public class GameplayConfigService
    {
        public GameplayConfig Config { get; }

        public GameplayConfigService(GameplayConfig config)
        {
            Config = config;
        }
    }
}