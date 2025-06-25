using Game.Core;
using Game.Core.DI;
using Game.Utility.LoadingScreen;
using System.Collections;

namespace Game.Utility.SceneManagment
{
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;
        private readonly DIContainer _globalContainer;

        public SceneSwitcherService(
            SceneLoaderService sceneLoaderService,
            ILoadingScreen loadingScreen,
            DIContainer globalContainer)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _globalContainer = globalContainer;
        }

        public IEnumerator SwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap sceneBootstrap = UnityEngine.Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap ==  null) 
                throw new System.NullReferenceException($"{nameof(sceneBootstrap)} not found");

            var sceneContainer = new DIContainer(_globalContainer);

            sceneBootstrap.ProcessRegistrations(sceneContainer, sceneArgs);

            sceneContainer.Inizialize();

            yield return sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}