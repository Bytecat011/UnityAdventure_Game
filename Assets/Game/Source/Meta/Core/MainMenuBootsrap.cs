using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System.Collections;
using Game.Meta.MainMenu;

namespace Game.Meta.Core
{
    public class MainMenuBootsrap : SceneBootstrap
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
        }
    }
}