using System;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.UI.LevelStatistics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenLevelsMenuButtonClicked;
        
        [field: SerializeField] public IconTextListView ResourcesView { get; private set; }

        [field: SerializeField] public LevelStatisticsView LevelStatisticsView { get; private set; }
        [field: SerializeField] public ButtonView ResetLevelStatisticsView { get; private set; }
        
        [SerializeField] private Button _openLevelsMenuButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
        }
        
        private void OnOpenLevelsMenuButtonClicked() => OpenLevelsMenuButtonClicked?.Invoke();
    }
}