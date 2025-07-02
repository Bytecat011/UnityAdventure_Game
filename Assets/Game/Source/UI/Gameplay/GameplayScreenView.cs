using System;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.UI.Gameplay.TypingGameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextListView ResourcesView { get; private set; }
        [field: SerializeField] public TypingGameplayView TypingGameplayView { get; private set; }
        
        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }
}