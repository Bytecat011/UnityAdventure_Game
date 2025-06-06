using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Game.Utility.Configs
{
    public class ConfigManager
    {
        private readonly Dictionary<Type, object> _configs = new();

        private readonly IConfigLoader[] _loaders;

        public ConfigManager(params IConfigLoader[] loaders)
        {
            _loaders = loaders;
        }

        public IEnumerator LoadAsync()
        {
            _configs.Clear();

            foreach (var loader in _loaders)
            {
                yield return loader.LoadAsync(loadedConfigs => _configs.AddRange(loadedConfigs));
            }
        }

        public T GetConfig<T>() where T : class
        {
            if (!_configs.TryGetValue(typeof(T), out var config))
            {
                throw new InvalidOperationException($"Not found config by {typeof(T)}");
            }
            return (T)config;
        }
    }
}