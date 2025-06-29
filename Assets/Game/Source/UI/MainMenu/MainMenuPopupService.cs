using Game.UI.Core;
using UnityEngine;

namespace Game.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        private readonly MainMenuUIRoot _uiRoot;
        
        public MainMenuPopupService(
            ViewsFactory viewsFactory, 
            ProjectPresentersFactory presentersFactory,
            MainMenuUIRoot uiRoot)
            : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;
    }
}