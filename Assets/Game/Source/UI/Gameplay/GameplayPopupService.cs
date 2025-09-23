using System;
using Game.Gameplay.EntitiesCore;
using Game.UI.Core;
using Game.UI.Gameplay.AbilitySelectPopup;
using Game.UI.Gameplay.ResultsPopup;
using UnityEngine;

namespace Game.UI.Gameplay
{
    public class GameplayPopupService : PopupService
    {
        private readonly GameplayUIRoot _uiRoot;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        
        public GameplayPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory presentersFactory, 
            GameplayUIRoot uiRoot,
            GameplayPresentersFactory gameplayPresentersFactory)
            : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
            _gameplayPresentersFactory = gameplayPresentersFactory;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;

        public WinPopupPresenter OpenWinPopup(Action closedCallback = null)
        {
            WinPopupView view = ViewsFactory.Create<WinPopupView>(ViewIDs.WinPopup, PopupLayer);

            WinPopupPresenter popup = _gameplayPresentersFactory.CreateWinPopupPresenter(view);
            
            OnPopupCreated(popup, view, closedCallback);
            
            return popup;
        }
        
        public LosePopupPresenter OpenLosePopup(Action closedCallback = null)
        {
            LosePopupView view = ViewsFactory.Create<LosePopupView>(ViewIDs.LosePopup, PopupLayer);

            LosePopupPresenter popup = _gameplayPresentersFactory.CreateLosePopupPresenter(view);
            
            OnPopupCreated(popup, view, closedCallback);
            
            return popup;
        }

        public AbilitySelectPopupPresenter OpenAbilitySelectPopup(Entity entity, Action closedCallback = null)
        {
            var view = ViewsFactory.Create<AbilitySelectPopupView>(ViewIDs.AbilitySelectPopup);

            var popup = _gameplayPresentersFactory.CreateAbilitySelectPopupPresenter(view, entity);
            
            OnPopupCreated(popup, view, closedCallback);
            
            return popup;
        }
    }
}