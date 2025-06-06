using Game.Core;
using Game.Core.DI;
using Game.Gameplay.Core;
using Game.Meta.Config;
using Game.Meta.UI;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagment;
using Game.Utility.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Game.Meta.Core
{
    public class MainMenuBootsrap : SceneBootstrap
    {
        private DIContainer _container;

        [SerializeField] private MainMenuLevelsUI _levelsUI;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            MainMenuContextRegistrations.Process(container);
        }

        public override IEnumerator Initialize()
        {
            ConfigManager configManager = _container.Resolve<ConfigManager>();
            LevelsListConfig levelsList = configManager.GetConfig<LevelsListConfig>();

            _levelsUI.Setup(levelsList);

            _levelsUI.LevelSelected += OnLevelSelected;

            yield break;
        }

        private void OnLevelSelected(int levelNumber)
        {
            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            ICoroutineRunner coroutineRunner = _container.Resolve<ICoroutineRunner>();

            coroutineRunner.StartTask(sceneSwitcher.SwitchTo(Scenes.Gameplay, new GameplayInputArgs(levelNumber)));
        }

        public override void Run()
        {
        }

        private void OnDestroy()
        {
            if (_levelsUI != null)
                _levelsUI.LevelSelected -= OnLevelSelected;
        }
    }
}