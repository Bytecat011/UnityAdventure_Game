using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Gameplay.ResultsPopup
{
    public class LosePopupView : PopupViewBase
    {
        public event Action ExitClicked;
        public event Action RestartClicked;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _restartButton;
        
        public void SetTitle(string title) => _title.text = title;
        
        public void OnExitClicked() => ExitClicked?.Invoke();
        public void OnRestartClicked() => RestartClicked?.Invoke();

        protected override void OnPreShow()
        {
            base.OnPreShow();
            
            _exitButton.onClick.AddListener(OnExitClicked);
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _exitButton.onClick.RemoveListener(OnExitClicked);
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitClicked);
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }
    }
}