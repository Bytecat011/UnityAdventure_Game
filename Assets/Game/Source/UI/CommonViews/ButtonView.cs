using System;
using Game.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.CommonViews
{
    public class ButtonView : MonoBehaviour, IView
    {
        public Action Clicked;

        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        void OnClick() => Clicked?.Invoke();
    }
}