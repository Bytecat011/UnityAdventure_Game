using Game.Core.DI;
using Game.Gameplay.TypingGameplay;

namespace Game.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayScreenPresenter CreateGameplayScreen(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view,
                _container.Resolve<ProjectPresentersFactory>(),
                _container.Resolve<GameplayPopupService>(),
                _container.Resolve<TypingGameMode>());
        }
    }
}