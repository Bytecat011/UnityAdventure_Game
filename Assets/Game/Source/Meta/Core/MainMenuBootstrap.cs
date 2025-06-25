using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System.Collections;

namespace Game.Meta.Core
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            MainMenuContextRegistrations.Process(container);
        }

        public override IEnumerator Initialize()
        {
            yield break;
        }

        public override void Run()
        {
        }
    }
}