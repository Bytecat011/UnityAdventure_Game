using Game.Gameplay.Config;
using System;
using UnityEngine;

namespace Game.Meta.Config
{
    [Serializable]
    public class LevelConfig
    {
        [SerializeField] private string _title;
        [SerializeField] private GameplayConfig _config;

        public string Title => _title;
        public GameplayConfig Config => _config;
    }
}