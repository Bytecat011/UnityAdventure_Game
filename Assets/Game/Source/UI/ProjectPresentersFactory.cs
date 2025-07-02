using Game.Configs;
using Game.Core.DI;
using Game.Gameplay.TypingGameplay;
using Game.Meta.Features.Resources;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.UI.LevelsMenuPopup;
using Game.UI.Resources;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;
using Game.Utility.Reactive;
using Game.Utility.SceneManagement;

namespace Game.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public ResourcePresenter CreateResourcePresenter(
            IconTextView view,
            IReactiveVariable<int> resource,
            ResourceType resourceType)
        {
            return new ResourcePresenter(
                resource, 
                resourceType,
                _container.Resolve<ConfigManager>().GetConfig<ResourceIconsConfig>(),
                view);
        }

        public ResourcesPresenter CreateResourcesPresenter(IconTextListView view)
        {
            return new ResourcesPresenter(
                _container.Resolve<ResourceStorage>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }

        public TestPopupPresenter CreateTestPopupPresenter(TestPopupView view)
        {
            return new TestPopupPresenter(
                view,
                _container.Resolve<ICoroutineRunner>());
        }

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, GameplayModeType gameplayMode)
        {
            return new LevelTilePresenter(
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutineRunner>(),
                gameplayMode,
                view);
        }

        public LevelsMenuPopupPresenter CreateLevelMenuPopupPresenter(LevelsMenuPopupView view)
        {
            return new LevelsMenuPopupPresenter(
                _container.Resolve<ICoroutineRunner>(),
                _container.Resolve<ConfigManager>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }
    }
}