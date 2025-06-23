using Game.Meta.Features.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Meta/Resources/StartResourcesDataConfig", fileName = "StartResourcesDataConfig")]
    public class StartResourcesDataConfig : ScriptableObject
    {
        [SerializeField] private List<ResourceConfig> _values;

        public int GetValueFor(ResourceType resourceType)
            => _values.First(config => config.Type == resourceType).Value;

        [Serializable]
        private class ResourceConfig
        {
            [field: SerializeField] public ResourceType Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}