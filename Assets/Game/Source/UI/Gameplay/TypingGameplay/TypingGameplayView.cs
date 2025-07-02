using Game.UI.Core;
using TMPro;
using UnityEngine;

namespace Game.UI.Gameplay.TypingGameplay
{
    public class TypingGameplayView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _targetText;
        [SerializeField] private TMP_Text _inputText;
        
        public void SetTargetText(string text) => _targetText.text = text;
        
        public void SetInputText(string text) => _inputText.text = text;
    }
}