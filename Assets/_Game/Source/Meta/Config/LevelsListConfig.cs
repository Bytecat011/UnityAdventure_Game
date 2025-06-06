using System.Collections.Generic;
using UnityEngine;

namespace Game.Meta.Config
{
    [CreateAssetMenu(fileName = "New Levels List", menuName = "Gameplay/LevelsList")]
    public class LevelsListConfig : ScriptableObject
    {
        [field: SerializeField]
        private List<LevelConfig> _levels = new();

        public IReadOnlyList<LevelConfig> Levels => _levels;
    }
}