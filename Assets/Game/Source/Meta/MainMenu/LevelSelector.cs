using System.Collections;
using Game.Gameplay.Core;
using Game.Gameplay.TypingGameplay;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using UnityEngine;

namespace Game.Meta.MainMenu
{
    public class LevelSelector
    {
        private const KeyCode NumbersGameModeKey = KeyCode.Alpha1;
        private const KeyCode LettersGameModeKey = KeyCode.Alpha2;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneSwitcherService _sceneSwitcher;

        public LevelSelector(ICoroutineRunner coroutineRunner, SceneSwitcherService sceneSwitcher)
        {
            _coroutineRunner = coroutineRunner;
            _sceneSwitcher = sceneSwitcher;
        }

        public void Start()
        {
            _coroutineRunner.StartTask(SelectLevelCoroutine());
        }

        private IEnumerator SelectLevelCoroutine()
        {
            Debug.Log($"Press [{NumbersGameModeKey}] to select Numbers");
            Debug.Log($"Press [{LettersGameModeKey}] to select Letters");
            
            while (true)
            {
                if (Input.GetKeyDown(NumbersGameModeKey))
                {
                    OnLevelSelected(GameplayModeType.Numbers);
                    yield break;
                }
                if (Input.GetKeyDown(LettersGameModeKey))
                {
                    OnLevelSelected(GameplayModeType.Letters);
                    yield break;
                }
                yield return null;
            }
        }

        private void OnLevelSelected(GameplayModeType mode)
        {
            _coroutineRunner.StartTask(_sceneSwitcher.SwitchTo(Scenes.Gameplay, new GameplayInputArgs(mode)));
        }
    }
}