using System.Collections.Generic;
using Game.Configs.Gameplay.Levels;
using Game.UI.Core;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;

namespace Game.UI.LevelsMenuPopup
{
    public class LevelsMenuPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "Levels";

        private readonly ConfigManager _configManager;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly LevelsMenuPopupView _view;

        private readonly List<LevelTilePresenter> _levelTilePresenters = new();

        public LevelsMenuPopupPresenter(
            ICoroutineRunner coroutineRunner,
            ConfigManager configManager,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            LevelsMenuPopupView view) : base(coroutineRunner)
        {
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
            _configManager = configManager;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

            var levelsListConfig = _configManager.GetConfig<LevelsListConfig>();

            for (int i = 0; i < levelsListConfig.Levels.Count; i++)
            {
                var levelTileView = _viewsFactory.Create<LevelTileView>(ViewIDs.LevelTile);

                _view.LevelTilesListView.Add(levelTileView);

                var levelTilePresenter = _presentersFactory.CreateLevelTilePresenter(levelTileView, i + 1);

                levelTilePresenter.Initialize();

                _levelTilePresenters.Add(levelTilePresenter);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var levelTilePresenter in _levelTilePresenters)
            {
                _view.LevelTilesListView.Remove(levelTilePresenter.View);
                _viewsFactory.Release(levelTilePresenter.View);
                levelTilePresenter.Dispose();
            }

            _levelTilePresenters.Clear();
        }

        protected override void OnPreShow()
        {
            base.OnPreShow();

            foreach (var levelTilePresenter in _levelTilePresenters)
                levelTilePresenter.Subscribe();
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            foreach (var levelTilePresenter in _levelTilePresenters)
                levelTilePresenter.Unsubscribe();
        }
    }
}