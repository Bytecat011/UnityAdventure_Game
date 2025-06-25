using Game.Utility.Reactive;
using System.Collections.Generic;
using System;
using Game.Utility.DataManagment.DataProviders;
using Game.Data;

namespace Game.Meta.Features.Resources
{
    public class ResourceStorage : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<ResourceType, ReactiveVariable<int>> _resources;

        public ResourceStorage(
            Dictionary<ResourceType, ReactiveVariable<int>> resources,
            PlayerDataProvider playerDataProvider)
        {
            _resources = resources;
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public IReactiveVariable<int> GetResource(ResourceType type) => _resources[type];

        public bool IsEnough(ResourceType type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _resources[type].Value >= amount;
        }

        public void Add(ResourceType type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _resources[type].Value += amount;
        }

        public void Spend(ResourceType type, int amount)
        {
            if (IsEnough(type, amount) == false)
                throw new InvalidOperationException($"Not enough: {type}");

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _resources[type].Value -= amount;
        }

        public void WriteTo(PlayerData data)
        {
            foreach (var (type, value) in _resources)
            {
                if (data.ResourceData.ContainsKey(type))
                    data.ResourceData[type] = value.Value;
                else
                    data.ResourceData.Add(type, value.Value);
            }
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (var (type, value) in data.ResourceData)
            {
                if (_resources.ContainsKey(type))
                    _resources[type].Value = value;
                else
                    _resources.Add(type, new ReactiveVariable<int>(value));
            }
        }
    }
}