using Game.Configs.Gameplay.Levels;
using Game.Utility.Configs;

namespace Game.Meta.Features.LevelSelection
{
    public interface ILevelSelector
    {
        public LevelConfig GetLevel();
    }

    public class RandomLevelSelector : ILevelSelector
    {
        private readonly ConfigManager _configManager;

        public RandomLevelSelector(ConfigManager configManager)
        {
            _configManager = configManager;
        }

        public LevelConfig GetLevel()
        {
            var levelsList = _configManager.GetConfig<LevelsListConfig>();
            return levelsList.Levels[UnityEngine.Random.Range(0, levelsList.Levels.Count)];
        }
    }
}