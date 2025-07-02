using Game.Gameplay.TypingGameplay;
using UnityEngine;

namespace Game.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public GameplayModeType GameplayMode { get; private set; }
        [field: SerializeField] public int WordLength { get; private set; }
        [field: SerializeField] public string AllowedCharacters { get; private set; }
    }
}