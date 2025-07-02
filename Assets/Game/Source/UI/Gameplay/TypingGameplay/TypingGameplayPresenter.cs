using Game.Gameplay.TypingGameplay;
using Game.UI.Core;

namespace Game.UI.Gameplay.TypingGameplay
{
    public class TypingGameplayPresenter : IPresenter
    {
        private readonly TypingGameMode _gameMode;
        private readonly TypingGameplayView _view;

        public TypingGameplayPresenter(
            TypingGameMode gameMode, 
            TypingGameplayView view)
        {
            _gameMode = gameMode;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetTargetText(_gameMode.TargetWord);
            _view.SetInputText("");
            _gameMode.Changed += OnGameModeChanged;
        }

        public void Dispose()
        {
            _gameMode.Changed -= OnGameModeChanged;
        }
        
        private void OnGameModeChanged()
        {
            _view.SetInputText(_gameMode.TargetWord.Substring(0, _gameMode.CompletedCharsCount));
        }
    }
}