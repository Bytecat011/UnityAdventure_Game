using System.Collections.Generic;
using Game.Configs.Gameplay.Stages;
using UnityEngine;

namespace Game.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<StageConfig> _stageConfigs;
        
        public IReadOnlyList<StageConfig> StageConfigs => _stageConfigs;
    }
}