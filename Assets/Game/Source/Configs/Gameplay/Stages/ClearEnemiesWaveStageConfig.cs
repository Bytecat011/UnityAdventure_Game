using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs.Gameplay.Stages
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Stages/NewClearEnemiesWaveStageConfig", fileName = "ClearEnemiesWaveStageConfig")]
    public class ClearEnemiesWaveStageConfig : StageConfig
    {
        [field: SerializeField] public float EnemySpawnCooldown { get; private set; }
        
        [SerializeField] private List<EnemyItemConfig> _enemyItems;

        public IReadOnlyList<EnemyItemConfig> EnemyItems => _enemyItems;
    }
}