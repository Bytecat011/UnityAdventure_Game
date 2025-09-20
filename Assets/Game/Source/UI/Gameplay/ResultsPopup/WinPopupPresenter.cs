using Game.UI.Core;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;

namespace Game.UI.Gameplay.ResultsPopup
{
    public class WinPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU WIN!";

        private readonly WinPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutineRunner _coroutineRunner;
        
        public WinPopupPresenter(
            ICoroutineRunner coroutineRunner,
            WinPopupView view, 
            SceneSwitcherService sceneSwitcher) : base(coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _view = view;
            _sceneSwitcher = sceneSwitcher;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(TitleName);

            _view.ContinueClicked += OnContinueClicked;
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _view.ContinueClicked -= OnContinueClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _view.ContinueClicked -= OnContinueClicked;
        }

        private void OnContinueClicked()
        {
            _coroutineRunner.StartTask(_sceneSwitcher.SwitchTo(Scenes.MainMenu));
            OnCloseRequest();
        }
    }
}