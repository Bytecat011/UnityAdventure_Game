using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Config
{
    [Serializable]
    public class GameplayConfig
    {
        [field: SerializeField] private int _wordLength = 5;
        [field: SerializeField] private List<char> _charsList = new();

        public IReadOnlyList<char> CharsList => _charsList;
        public int WordLength => _wordLength;
    }
}