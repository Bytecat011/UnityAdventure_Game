using Game.Utility.Reactive;
using System.Collections.Generic;
using System;

namespace Game.Meta.Features.Resources
{
    public class ResourceStorage
    {
        private readonly Dictionary<ResourceType, ReactiveVariable<int>> _resources;

        public ResourceStorage(Dictionary<ResourceType, ReactiveVariable<int>> resources)
        {
            _resources = resources;
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
    }
}