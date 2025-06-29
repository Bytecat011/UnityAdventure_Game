using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System.Collections;
using Game.Meta.Features.Resources;

namespace Game.Meta.Core
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        
        private ResourceStorage _resourceStorage;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            MainMenuContextRegistrations.Process(container);
        }

        public override IEnumerator Initialize()
        {
            _resourceStorage = _container.Resolve<ResourceStorage>();
            yield break;
        }

        public override void Run()
        {
        }
    }
}