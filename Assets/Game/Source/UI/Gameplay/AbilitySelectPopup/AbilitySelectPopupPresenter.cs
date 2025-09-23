using System.Collections.Generic;
using Game.Configs.Gameplay.Abilities;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AbilityDropping;
using Game.UI.Core;
using Game.Utility.CoroutineManagement;

namespace Game.UI.Gameplay.AbilitySelectPopup
{
    public class AbilitySelectPopupPresenter : PopupPresenterBase
    {
        private const int AbilityCount = 3;

        private const string Title = "Level {0} in this advanture";
        private const string SelectAbilityText = "Select ability";
        
        private readonly AbilitySelectPopupView _view;
        
        private readonly Entity _entity;
        private readonly AbilityDropService _abilityDropper;
        private readonly GameplayPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        
        private List<SelectableAbilityPresenter> _presenters = new();
        private SelectableAbilityPresenter _selectedPresenter;
        
        public AbilitySelectPopupPresenter(
            ICoroutineRunner coroutineRunner, 
            AbilitySelectPopupView view,
            Entity entity,
            GameplayPresentersFactory presentersFactory,
            AbilityDropService abilityDropper,
            ViewsFactory viewsFactory) : base(coroutineRunner)
        {
            _view = view;
            _entity = entity;
            _presentersFactory = presentersFactory;
            _abilityDropper = abilityDropper;
            _viewsFactory = viewsFactory;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(string.Format(Title, 2));
            _view.SetAdditionalText(SelectAbilityText);
            _view.SelectButtonOff();

            _view.SelectButtonClicked += OnSelectButtonClicked;
            
            List<AbilityConfig> dropOptions = _abilityDropper.Drop(AbilityCount, _entity);

            for (int i = 0; i < dropOptions.Count; i++)
            {
                SelectableAbilityView abilityView = _viewsFactory.Create<SelectableAbilityView>(ViewIDs.SelectableAbilityView);
                
                _view.AbilityListView.Add(abilityView);
                
                var presenter = _presentersFactory
                    .CreateSelectableAbilityPresenter(dropOptions[i], abilityView, _entity);

                presenter.Selected += OnPresenterSelected;
                presenter.Initialize();
                
                _presenters.Add(presenter);
            }
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _view.SelectButtonOff();
            
            _view.SelectButtonClicked -= OnSelectButtonClicked;

            foreach (var presenter in _presenters)
            {
                presenter.Selected -= OnPresenterSelected;
                _view.AbilityListView.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose();
            }
            
            _presenters.Clear();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _view.SelectButtonOff();
            
            _view.SelectButtonClicked -= OnSelectButtonClicked;
            
            foreach (var presenter in _presenters)
                presenter.Selected -= OnPresenterSelected;
        }

        private void OnPresenterSelected(SelectableAbilityPresenter presenter)
        {
            _view.SelectButtonOn();
            _view.AbilityListView.Select(presenter.View);
            _selectedPresenter = presenter;
        }

        private void OnSelectButtonClicked()
        {
            _selectedPresenter.Provide();
            OnCloseRequest();
        }
    }
}