using Game.Core.DI;
using Game.Gameplay.Core;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.StagesFeature;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.UI.Gameplay.HealthDisplay;
using Game.UI.Gameplay.ResultsPopup;
using Game.UI.Gameplay.Stages;
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
            return new GameplayScreenPresenter(view, _container.Resolve<GameplayPresentersFactory>());
        }

        public EntityHealthPresenter CreateEntityHealthPresenter(Entity entity, BarWithText view)
        {
            return new EntityHealthPresenter(entity, view);
        }
        
        public StagePresenter CreateStagePresenter(IconTextView view)
        {
            return new StagePresenter(view, _container.Resolve<StageProviderService>());
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

        public EntitiesHealthDisplayPresenter CreateEntitiesHealthDisplayPresenter(EntitiesHealthDisplay view)
        {
            return new EntitiesHealthDisplayPresenter(
                _container.Resolve<EntitiesWorld>(),
                view,
                _container.Resolve<ViewsFactory>(),
                this);
        }
    }
}