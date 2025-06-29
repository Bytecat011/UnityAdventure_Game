using Game.Configs;
using Game.Meta.Features.Resources;
using Game.Utility.Configs;
using Game.Utility.DataManagement;
using Game.Utility.DataManagement.DataProviders;
using Game.Utility.Reactive;
using System;
using System.Collections.Generic;

namespace Game.Data
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigManager _configManager;

        public PlayerDataProvider(ISaveLoadService saveLoadService, ConfigManager configManager) : base(saveLoadService)
        {
            _configManager = configManager;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData
            {
                ResourceData = InitResourceData(),
                CompletedLevels = new()
            };
        }

        private Dictionary<ResourceType, int> InitResourceData()
        {
            Dictionary<ResourceType, int> resourceData = new();

            StartResourcesDataConfig resourcesConfig = _configManager.GetConfig<StartResourcesDataConfig>();

            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
                resourceData[resourceType] =resourcesConfig.GetValueFor(resourceType);

            return resourceData;
        }
    }
}