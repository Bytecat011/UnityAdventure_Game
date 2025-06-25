using System;
using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System.Collections;
using Game.Meta.MainMenu;

namespace Game.Meta.Core
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private LevelSelector _levelSelector;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            MainMenuContextRegistrations.Process(container);
        }

        public override IEnumerator Initialize()
        {
            _levelSelector = _container.Resolve<LevelSelector>();
            
            yield break;
        }

        public override void Run()
        {
            _levelSelector.Start();
            _container.Resolve<PlayerGoldDisplayService>().Start();
            _container.Resolve<LevelsStatisticsResetter>().Start();
            _container.Resolve<LevelsStatisticsDisplayService>().Start();
        }

        private void OnDestroy()
        {
            _container.Resolve<PlayerGoldDisplayService>().Dispose();
            _container.Resolve<LevelsStatisticsResetter>().Dispose();
            _container.Resolve<LevelsStatisticsDisplayService>().Dispose();
        }
    }
}