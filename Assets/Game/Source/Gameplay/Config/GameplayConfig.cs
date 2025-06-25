using System;
using UnityEngine;

namespace Game.Gameplay.Config
{
    [Serializable]
    public class GameplayConfig
    {
        [field: SerializeField] public int WordLength { get; private set; }
        [field: SerializeField] public string AllowedCharacters { get; private set; }
    }
}