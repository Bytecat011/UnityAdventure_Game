using Game.Configs.Gameplay.Levels;
using Game.Utility.SceneManagement;

namespace Game.Gameplay.Core
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(LevelConfig levelConfig)
        {
            LevelConfig = levelConfig;
        }

        public LevelConfig LevelConfig { get; }
    }
}