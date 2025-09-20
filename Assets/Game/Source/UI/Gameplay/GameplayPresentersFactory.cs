using Game.Core.DI;
using Game.Gameplay.Core;
using Game.UI.Gameplay.ResultsPopup;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;

namespace Game.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;
        private readonly GameplayInputArgs _gameplayInputArgs;

        public GameplayPresentersFactory(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;
            _gameplayInputArgs = gameplayInputArgs;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(view);
        }

        public WinPopupPresenter CreateWinPopupPresenter(WinPopupView view)
        {
            return new WinPopupPresenter(
                _container.Resolve<ICoroutineRunner>(),
                view,
                _container.Resolve<SceneSwitcherService>()
            );
        }

        public LosePopupPresenter CreateLosePopupPresenter(LosePopupView view)
        {
            return new LosePopupPresenter(
                _container.Resolve<ICoroutineRunner>(),
                view,
                _container.Resolve<SceneSwitcherService>(),
                _gameplayInputArgs);
        }
    }
}