using Game.Core.DI;
using Game.Utility.Assets;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagment;
using Game.Utility.LoadingScreen;
using Game.Utility.SceneManagment;
using System;
using System.Collections.Generic;

namespace Game.Core.EntryPoint
{
    public static class GlobalContextRegistration
    {
        private static Dictionary<Type, string> _configsResourcesPaths = new Dictionary<Type, string>
        {
        };

        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateConfigManager);

            container.RegisterAsSingle(CreateResourceAssetLoader);

            container.RegisterAsSingle<ICoroutineRunner>(CreateCoroutineRunner);

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);


        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        private static ConfigManager CreateConfigManager(DIContainer c)
        {
            var resourceAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            var resourceConfigLoader = new ResourcesConfigLoader(resourceAssetsLoader, _configsResourcesPaths);

            return new ConfigManager(resourceConfigLoader);
        }

        private static ResourcesAssetsLoader CreateResourceAssetLoader(DIContainer c)
            => new ResourcesAssetsLoader();

        private static CoroutineRunner CreateCoroutineRunner(DIContainer c)
        {
            var resourceAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            var coroutineRunnerPrefab = resourceAssetsLoader.Load<CoroutineRunner>("Utilities/CoroutineRunner");

            return UnityEngine.Object.Instantiate(coroutineRunnerPrefab);
        }

        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            var resourceAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            var standardLoadingScreenPrefab = resourceAssetsLoader.Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");

            return UnityEngine.Object.Instantiate(standardLoadingScreenPrefab);
        }
    }
}