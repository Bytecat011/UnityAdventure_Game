using Game.UI.Core;
using UnityEngine;

namespace Game.UI.Gameplay
{
    public class GameplayPopupService: PopupService
    {
        private readonly GameplayUIRoot _uiRoot;
        
        public GameplayPopupService(
            ViewsFactory viewsFactory, 
            ProjectPresentersFactory presentersFactory,
            GameplayUIRoot uiRoot)
            : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;
    }
}