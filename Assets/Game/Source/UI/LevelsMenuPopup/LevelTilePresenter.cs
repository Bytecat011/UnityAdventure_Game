using Game.Gameplay.Core;
using Game.Gameplay.TypingGameplay;
using Game.UI.Core;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using UnityEngine;

namespace Game.UI.LevelsMenuPopup
{
    public class LevelTilePresenter : ISubscribePresernter
    {
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly GameplayModeType _gameplayMode;

        private readonly LevelTileView _view;

        public LevelTilePresenter(
            SceneSwitcherService sceneSwitcherService,
            ICoroutineRunner coroutineRunner,
            GameplayModeType gameplayMode,
            LevelTileView view)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutineRunner = coroutineRunner;
            _gameplayMode = gameplayMode;
            _view = view;
        }

        public LevelTileView View => _view;

        public void Initialize()
        {
            _view.SetLevel(_gameplayMode.ToString());

            _view.SetActive();
        }
        
        public void Dispose()
        {
            _view.Clicked -= OnViewClicked;
        }

        public void Subscribe()
        {
            _view.Clicked += OnViewClicked;
        }

        public void Unsubscribe()
        {
            _view.Clicked -= OnViewClicked;
        }
        
        private void OnViewClicked()
        {
            _coroutineRunner.StartTask(_sceneSwitcherService.SwitchTo(
                Scenes.Gameplay,
                new GameplayInputArgs(_gameplayMode)));
        }
    }
}