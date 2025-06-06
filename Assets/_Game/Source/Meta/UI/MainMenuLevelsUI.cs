using Game.Meta.Config;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Game.Meta.UI
{
    public class MainMenuLevelsUI : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons = new();

        public event Action<int> LevelSelected;

        public void Setup(LevelsListConfig levelsList)
        {
            for (int i = 0; i < levelsList.Levels.Count; i++)
            {
                if (i >= buttons.Count)
                    break;

                int localLevelIndex = i;

                LevelConfig level = levelsList.Levels[i];
                Button button = buttons[i];

                var buttonText = button.GetComponentInChildren<TMP_Text>();
                if (buttonText != null) 
                    buttonText.text = level.Title;

                button.onClick.AddListener(() => LevelSelected?.Invoke(localLevelIndex));
            }
        }

        private void OnDestroy()
        {
            foreach (var button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}