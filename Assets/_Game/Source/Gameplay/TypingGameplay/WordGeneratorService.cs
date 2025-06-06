using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Game.Gameplay.TypingGameplay
{
    public class WordGeneratorService
    {
        private readonly IReadOnlyList<char> _charSet;

        public WordGeneratorService(IReadOnlyList<char> charSet)
        {
            _charSet = charSet;
        }

        public string GenerateWord(int length)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(_charSet[Random.Range(0, _charSet.Count)]);
            }

            return sb.ToString();
        }
    }
}