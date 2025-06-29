using Game.Utility.Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Configs;
using Game.Configs.Gameplay.Levels;
using UnityEngine;

namespace Game.Utility.Configs
{
    public class ResourcesConfigLoader : IConfigLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourcesPaths = new Dictionary<Type, string>
        {
            { typeof(StartResourcesDataConfig), "Configs/Meta/Resources/StartResourcesDataConfig" },
            { typeof(ResourceIconsConfig), "Configs/Meta/Resources/ResourceIconsConfig" },
            { typeof(LevelsListConfig), "Configs/Gameplay/Levels/LevelsListConfig" }
        };

        public ResourcesConfigLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (var (configType, configPath) in _configsResourcesPaths)
            {
                var config = _resources.Load<ScriptableObject>(configPath);
                loadedConfigs.Add(configType, config);
                yield return null;
            }

            onConfigLoaded?.Invoke(loadedConfigs);
        }
    }
}