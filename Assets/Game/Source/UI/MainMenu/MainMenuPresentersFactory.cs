using Game.Core.DI;
using Game.Meta.Features.LevelSelection;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;

namespace Game.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ProjectPresentersFactory>(),
                _container.Resolve<ILevelSelector>(),
                _container.Resolve<ICoroutineRunner>(),
                _container.Resolve<SceneSwitcherService>());
        }
    }
}