using Game.Core.DI;
using Game.Data;
using Game.Meta.Features.Resources;
using Game.Utility.Assets;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;
using Game.Utility.DataManagement;
using Game.Utility.DataManagement.KeysStorage;
using Game.Utility.DataManagement.Serializers;
using Game.Utility.DataManagement.Storage;
using Game.Utility.LoadingScreen;
using Game.Utility.Reactive;
using Game.Utility.SceneManagement;
using System;
using System.Collections.Generic;
using Game.Meta.Features.LevelsProgression;
using Game.UI;
using Game.UI.Core;
using Game.Utility.Timer;
using UnityEngine;

namespace Game.Core.EntryPoint
{
    public static class GlobalContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateConfigManager);

            container.RegisterAsSingle(CreateResourceAssetLoader);

            container.RegisterAsSingle<ICoroutineRunner>(CreateCoroutineRunner);

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);

            container.RegisterAsSingle(CreateResourceStorage).NonLazy();

            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);

            container.RegisterAsSingle(CreatePlayerDataProvider);
            
            container.RegisterAsSingle(CreateProjectPresentersFactory);
            
            container.RegisterAsSingle(CreateViewsFactory);

            container.RegisterAsSingle(CreateTimerService);
            
            container.RegisterAsSingle(CreateLevelsProgressionService).NonLazy();
        }

        private static TimerServiceFactory CreateTimerService(DIContainer c)
            => new TimerServiceFactory(c);
        
        private static LevelsProgressionService CreateLevelsProgressionService(DIContainer c)
            => new LevelsProgressionService(c.Resolve<PlayerDataProvider>());
        
        private static ViewsFactory CreateViewsFactory(DIContainer c)
            => new ViewsFactory(c.Resolve<ResourcesAssetsLoader>());
        
        private static ProjectPresentersFactory CreateProjectPresentersFactory(DIContainer c)
            => new ProjectPresentersFactory(c);
        
        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
            => new PlayerDataProvider(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigManager>());

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer(Newtonsoft.Json.Formatting.Indented);
            IDataKeyStarage dataKeyStarage = new MapDataKeyStorage(new Dictionary<Type, string> {
                { typeof(PlayerData), "PlayerData" }
            });

            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            IDataStorage dataStorage = new LocalFileDataStorage(saveFolderPath, "json");

            return new SaveLoadService(dataSerializer, dataKeyStarage, dataStorage);
        }

        private static ResourceStorage CreateResourceStorage(DIContainer c)
        {
            Dictionary<ResourceType, ReactiveVariable<int>> resources = new();

            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
                resources[resourceType] = new ReactiveVariable<int>();

            return new ResourceStorage(resources, c.Resolve<PlayerDataProvider>());
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

            var resourceConfigLoader = new ResourcesConfigLoader(resourceAssetsLoader);

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