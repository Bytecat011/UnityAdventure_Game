using System;
using UnityEngine;

namespace Game.Gameplay.TypingGameplay
{
    public class TypingGameMode
    {
        public event Action Win;
        public event Action Lose;

        private readonly string _word;

        private bool _isActive = false;
        private int _completedCharsCount = 0;

        public bool IsActive => _isActive;
        public int CompletedCharsCount => _completedCharsCount;
        public string TargetWord => _word;

        public TypingGameMode(string word)
        {
            _word = word;
        }

        public void Start()
        {
            _completedCharsCount = 0;
            _isActive = true;
        }

        public void Update()
        {
            if (!_isActive)
                return;

            if (TryGetInputChar(out char inputChar))
            {
                if (inputChar == _word[_completedCharsCount])
                    _completedCharsCount++;
                else
                {
                    Lose?.Invoke();
                    _isActive = false;
                }
            }

            if (HandleWinCondition())
            {
                Win?.Invoke();
                _isActive = false;
            }
        }

        private bool HandleWinCondition() => _completedCharsCount == _word.Length;

        private bool TryGetInputChar(out char inputChar)
        {
            inputChar = default;

            if (Input.inputString.Length == 0)
                return false;

            inputChar = Input.inputString[0];
            return true;
        }
    }
}