using Game.Gameplay.Core;
using Game.Meta.Features.LevelsProgression;
using Game.UI.Core;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using UnityEngine;

namespace Game.UI.LevelsMenuPopup
{
    public class LevelTilePresenter : ISubscribePresernter
    {
        private readonly LevelsProgressionService _levelService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly int _levelNumber;

        private readonly LevelTileView _view;

        public LevelTilePresenter(
            LevelsProgressionService levelService,
            SceneSwitcherService sceneSwitcherService,
            ICoroutineRunner coroutineRunner,
            int levelNumber,
            LevelTileView view)
        {
            _levelService = levelService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutineRunner = coroutineRunner;
            _levelNumber = levelNumber;
            _view = view;
        }

        public LevelTileView View => _view;

        public void Initialize()
        {
            _view.SetLevel(_levelNumber.ToString());

            if (_levelService.CanPlay(_levelNumber))
            {
                if(_levelService.IsLevelCompleted(_levelNumber))
                    _view.SetComplete();
                else
                    _view.SetActive();
            }
            else
            {
                _view.SetBlock();
            }
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
            if (_levelService.CanPlay(_levelNumber) == false)
            {
                Debug.Log("Level locked, complete previous");
                return;
            }

            _coroutineRunner.StartTask(_sceneSwitcherService.SwitchTo(
                Scenes.Gameplay,
                new GameplayInputArgs(_levelNumber)));
        }
    }
}