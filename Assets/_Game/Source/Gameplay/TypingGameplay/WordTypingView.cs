using TMPro;
using UnityEngine;

namespace Game.Gameplay.TypingGameplay
{
    public class WordTypingView : MonoBehaviour
    {
        [field: SerializeField] private TMP_Text _wordText;

        private TypingGameMode _gameMode;
        private string _textTemplate;

        private int _lastCompletedCharsCount = 0;

        public void Initialize(TypingGameMode gameMode)
        {
            _gameMode = gameMode;
            _textTemplate = _wordText.text;
            _lastCompletedCharsCount = 0;
            UpdateView();
        }

        private void UpdateView()
        {
            string completedPart = _gameMode.TargetWord.Substring(0, _lastCompletedCharsCount);
            string restPart = _gameMode.TargetWord.Substring(_lastCompletedCharsCount);
            _wordText.text = string.Format(_textTemplate, completedPart, restPart);
        }

        private void Update()
        {
            if (_gameMode == null || _gameMode.IsActive == false)
                return;

            if (_lastCompletedCharsCount != _gameMode.CompletedCharsCount)
            {
                _lastCompletedCharsCount = _gameMode.CompletedCharsCount;
                UpdateView();
            }
        }
    }
}