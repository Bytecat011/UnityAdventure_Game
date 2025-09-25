using System;
using Game.Configs.Gameplay.Entities;
using UnityEngine;

namespace Game.Configs.Gameplay.Stages
{
    [Serializable]
    public class EnemyItemConfig
    {
        [field: SerializeField] public EntityConfig EnemyConfig { get; private set; }
    }
}