using Game.Gameplay.TypingGameplay;
using Game.Utility.SceneManagement;

namespace Game.Gameplay.Core
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameplayModeType gameplayMode)
        {
            GameplayMode = gameplayMode;
        }

        public GameplayModeType GameplayMode { get; }
    }
}