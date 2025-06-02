using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility.Configs
{
    public class ResourcesConfigLoader : IConfigLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourcesPaths;

        public ResourcesConfigLoader(ResourcesAssetsLoader resources, Dictionary<Type, string> configsResourcesPaths)
        {
            _resources = resources;
            _configsResourcesPaths = configsResourcesPaths ?? new();
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