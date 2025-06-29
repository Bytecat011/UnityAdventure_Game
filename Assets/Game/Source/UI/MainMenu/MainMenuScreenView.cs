using System;
using Game.UI.CommonViews;
using Game.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenTestPopupButtonClicked;
        
        [field: SerializeField] public IconTextListView ResourcesView { get; private set; }

        [SerializeField] private Button _openTestPopupButton;

        private void OnEnable()
        {
            _openTestPopupButton.onClick.AddListener(OnOpenTestPopupButtonClicked);
        }

        private void OnDisable()
        {
            _openTestPopupButton.onClick.RemoveListener(OnOpenTestPopupButtonClicked);
        }
        
        private void OnOpenTestPopupButtonClicked() => OpenTestPopupButtonClicked?.Invoke();
    }
}