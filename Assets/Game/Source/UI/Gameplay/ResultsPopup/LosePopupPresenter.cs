using Game.Gameplay.Core;
using Game.UI.Core;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;

namespace Game.UI.Gameplay.ResultsPopup
{
    public class LosePopupPresenter: PopupPresenterBase
    {
        private const string TitleName = "YOU LOSE!";

        private readonly LosePopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameplayInputArgs _currentLevelArgs;
        
        public LosePopupPresenter(
            ICoroutineRunner coroutineRunner,
            LosePopupView view, 
            SceneSwitcherService sceneSwitcher,
            GameplayInputArgs currentLevelArgs) : base(coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _view = view;
            _sceneSwitcher = sceneSwitcher;
            _currentLevelArgs = currentLevelArgs;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(TitleName);

            _view.ExitClicked += OnExitClicked;
            _view.RestartClicked += OnRestartClicked;
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _view.ExitClicked -= OnExitClicked;
            _view.RestartClicked -= OnRestartClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _view.ExitClicked -= OnExitClicked;
            _view.RestartClicked -= OnRestartClicked;
        }

        private void OnExitClicked()
        {
            _coroutineRunner.StartTask(_sceneSwitcher.SwitchTo(Scenes.MainMenu));
            OnCloseRequest();
        }
        
        private void OnRestartClicked()
        {
            _coroutineRunner.StartTask(_sceneSwitcher.SwitchTo(Scenes.Gameplay, new GameplayInputArgs(_currentLevelArgs.LevelNumber)));
            OnCloseRequest();
        }
    }
}