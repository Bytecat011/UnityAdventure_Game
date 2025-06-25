using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.TypingGameplay;
using UnityEngine;

namespace Game.Gameplay.Config
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Typing/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _values;

        public GameplayConfig GetConfigFor(GameplayModeType modeType)
            => _values.First(config => config.Type == modeType).Value;

        [Serializable]
        private class LevelConfig
        {
            [field: SerializeField] public GameplayModeType Type { get; private set; }
            [field: SerializeField] public GameplayConfig Value { get; private set; }
        }
    }
}