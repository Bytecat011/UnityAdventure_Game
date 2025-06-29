using Game.Utility.CoroutineManagement;

namespace Game.UI.Core
{
    public class TestPopupPresenter : PopupPresenterBase
    {
        private readonly TestPopupView _view;

        public TestPopupPresenter(TestPopupView view, ICoroutineRunner coroutineRunner) : base(coroutineRunner)
        {
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetText("Test title");
        }
    }
}