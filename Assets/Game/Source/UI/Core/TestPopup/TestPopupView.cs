using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.UI.Core
{
    public class TestPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _text;
        
        public void SetText(string text) => _text.text = text;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            animation
                .Append(_text
                    .DOFade(1, 0.25f)
                    .From(0f));

        }
    }
}