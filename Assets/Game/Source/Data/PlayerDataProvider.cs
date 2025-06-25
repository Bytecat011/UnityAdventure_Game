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
                LevelsStatistics = new LevelsStatistics()
            };
        }

        private Dictionary<ResourceType, int> InitResourceData()
        {
            Dictionary<ResourceType, int> resourceData = new();

            EconomyConfig economyConfig = _configManager.GetConfig<EconomyConfig>();

            resourceData[ResourceType.Gold] = economyConfig.StartGoldAmount;

            return resourceData;
        }
    }
}