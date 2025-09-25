using UnityEngine;

namespace Game.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewPlayerTowerConfig", fileName = "PlayerTowerConfig")]
    public class PlayerTowerConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/PlayerTower";
        [field: SerializeField, Min(0)] public float AttackProcessTime { get; private set; } = 1.5f;
        [field: SerializeField, Min(0)] public float AttackDelayTime { get; private set; } = 0.75f;
        [field: SerializeField, Min(0)] public float AttackCooldown { get; private set; } = 1f;
        [field: SerializeField, Min(0)] public float AttackDamage { get; private set; } = 10;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 2;
    }
}