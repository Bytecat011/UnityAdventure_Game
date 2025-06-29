using TMPro;
using UnityEngine;

namespace Game.UI.Core
{
    public class TestPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _text;
        
        public void SetText(string text) => _text.text = text;
    }
}