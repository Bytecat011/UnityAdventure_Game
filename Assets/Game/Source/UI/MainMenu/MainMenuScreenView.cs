using System;
using Game.UI.CommonViews;
using Game.UI.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action PlayButtonClicked;
        
        [field: SerializeField] public IconTextListView ResourcesView { get; private set; }

        [SerializeField] private Button _playButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }
        
        private void OnPlayButtonClicked() => PlayButtonClicked?.Invoke();
    }
}