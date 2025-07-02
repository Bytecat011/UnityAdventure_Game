using DG.Tweening;
using Game.UI.Core;
using TMPro;
using UnityEngine;

namespace Game.UI.LevelsMenuPopup
{
    public class LevelsMenuPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private LevelTilesListView _levelTilesListView;

        public LevelTilesListView LevelTilesListView => _levelTilesListView;

        public void SetTitle(string title) => _title.text = title;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            foreach (var levelTileView in _levelTilesListView.Elements)
            {
                animation.Append(levelTileView.Show());
            }
        }
    }
}