using System.Collections.Generic;
using Game.UI.Core;
using Game.UI.Resources;

namespace Game.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly MainMenuPopupService _popupService;
        
        private readonly List<IPresenter> _childPresenters = new();
        
        public MainMenuScreenPresenter(
            MainMenuScreenView screen, 
            ProjectPresentersFactory projectPresentersFactory, 
            MainMenuPopupService popupService)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _popupService = popupService;
        }

        public void Initialize()
        {
            _screen.OpenLevelsMenuButtonClicked += OnOpenLevelsMenuButtonClicked;
            
            CreateResources();

            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.OpenLevelsMenuButtonClicked -= OnOpenLevelsMenuButtonClicked;
            foreach (var presenter in _childPresenters)
                presenter.Dispose();
            
            _childPresenters.Clear();
        }

        private void OnOpenLevelsMenuButtonClicked()
        {
            _popupService.OpenLevelsMenuPopup();
        }
        
        private void CreateResources()
        {
            ResourcesPresenter resourcesPresenter =
                _projectPresentersFactory.CreateResourcesPresenter(_screen.ResourcesView);
            
            _childPresenters.Add(resourcesPresenter);
        }
    }
}