using System.Collections.Generic;
using Game.Gameplay.Core;
using Game.Meta.Features.LevelSelection;
using Game.UI.Core;
using Game.UI.Resources;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;

namespace Game.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly ILevelSelector _levelSelector;
        
        private readonly ICoroutineRunner _coroutineRunner;
        
        private readonly SceneSwitcherService _sceneSwitcherService;
        
        private readonly List<IPresenter> _childPresenters = new();
        
        public MainMenuScreenPresenter(
            MainMenuScreenView screen, 
            ProjectPresentersFactory projectPresentersFactory, 
            ILevelSelector levelSelector,
            ICoroutineRunner coroutineRunner, 
            SceneSwitcherService sceneSwitcherService)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _levelSelector = levelSelector;
            _coroutineRunner = coroutineRunner;
            _sceneSwitcherService = sceneSwitcherService;
        }

        public void Initialize()
        {
            _screen.PlayButtonClicked += OnPlayButtonClicked;
            
            CreateResources();

            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.PlayButtonClicked -= OnPlayButtonClicked;
            foreach (var presenter in _childPresenters)
                presenter.Dispose();
            
            _childPresenters.Clear();
        }

        private void OnPlayButtonClicked()
        {
            var level = _levelSelector.GetLevel();
            _coroutineRunner.StartTask(_sceneSwitcherService.SwitchTo(
                Scenes.Gameplay,
                new GameplayInputArgs(level)));
        }
        
        private void CreateResources()
        {
            ResourcesPresenter resourcesPresenter =
                _projectPresentersFactory.CreateResourcesPresenter(_screen.ResourcesView);
            
            _childPresenters.Add(resourcesPresenter);
        }
    }
}