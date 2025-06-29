using System;
using System.Collections.Generic;
using System.Linq;
using Game.Meta.Features.Resources;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Meta/Resources/ResourceIconsConfig", fileName = "ResourceIconsConfig")]
    public class ResourceIconsConfig : ScriptableObject
    {
        [SerializeField] private List<ResourceConfig> _configs;

        public Sprite GetSpriteFor(ResourceType resourceType)
            => _configs.First(config => config.Type == resourceType).Sprite;

        [Serializable]
        private class ResourceConfig
        {
            [field: SerializeField] public ResourceType Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}