using System.Collections.Generic;
using Game.Gameplay.TypingGameplay;
using Game.UI.Core;
using Game.UI.Gameplay.TypingGameplay;
using Game.UI.Resources;

namespace Game.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly GameplayPopupService _popupService;
        private readonly TypingGameMode _gameMode;
        
        private readonly List<IPresenter> _childPresenters = new();
        
        public GameplayScreenPresenter(
            GameplayScreenView screen,
            ProjectPresentersFactory projectPresentersFactory, 
            GameplayPopupService popupService,
            TypingGameMode gameMode)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _popupService = popupService;
            _gameMode = gameMode;
        }
        
        public void Initialize()
        {
            CreateResources();
            
            TypingGameplayPresenter gameplayPresenter = new TypingGameplayPresenter(_gameMode, _screen.TypingGameplayView);
            _childPresenters.Add(gameplayPresenter);
            
            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var presenter in _childPresenters)
                presenter.Dispose();
            
            _childPresenters.Clear();
        }
        
        private void CreateResources()
        {
            ResourcesPresenter resourcesPresenter =
                _projectPresentersFactory.CreateResourcesPresenter(_screen.ResourcesView);
            
            _childPresenters.Add(resourcesPresenter);
        }
    }
}